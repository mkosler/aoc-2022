using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day14 : IDay {
  private class Cave {
    public enum State { Air, Rock, Sand, Start }

    public int SandCount { get => _grid.Count(kv => kv.Value == State.Sand); }

    private readonly Dictionary<(int x, int y), State> _grid = new Dictionary<(int x, int y), State>();
    private int _floorY = 0;

    public Cave() {
      _grid[(500, 0)] = State.Start;
    }

    public void Set(int x, int y, State s) => Set((x, y), s);
    public void Set((int x, int y) p, State s) => _grid[p] = s;
    public bool IsAir(int x, int y) => IsAir((x, y));
    public bool IsAir((int x, int y) p) => !_grid.ContainsKey(p) || _grid[p] == State.Air;

    public void DrawLine(int x1, int y1, int x2, int y2) => DrawLine((x1, y1), (x2, y2));
    public void DrawLine((int x, int y) a, (int x, int y) b) {
      (int x, int y) dv = (b.x != a.x ? (b.x - a.x) / Math.Abs(b.x - a.x) : 0,
          b.y != a.y ? (b.y - a.y) / Math.Abs(b.y - a.y) : 0);

      (int x, int y) p = a;
      while (p != b) {
        Set(p, State.Rock);

        p = (p.x + dv.x, p.y + dv.y);
      }

      Set(b, State.Rock);
    }

    public void SetFloor() => _floorY = _grid.Where(kv => kv.Value == State.Rock).Max(kv => kv.Key.y) + 2;

    public bool Drop(int x = 500, int y = 0) => Drop((x, y));
    public bool Drop((int x, int y) sand) {
      if (!_grid.Keys.Any(p => p.x == sand.x && p.y > sand.y && !IsAir(p))) {
        // We are in freefall
        return true;
      }

      // Get highest non-air (State.Air or not in grid) point on the same x-axis
      sand = _grid
        .Where(p => p.Key.x == sand.x && p.Key.y > sand.y && !IsAir(p.Key))
        .OrderBy(p => p.Key.y)
        .First()
        .Key;

      // Step backwards once
      sand = (sand.x, sand.y - 1);

      // Check left diagonal
      if (IsAir(sand.x - 1, sand.y + 1)) return Drop(sand.x - 1, sand.y + 1);

      // Check right diagonal
      if (IsAir(sand.x + 1, sand.y + 1)) return Drop(sand.x + 1, sand.y + 1);

      // Neither diagonals are open, so settle the sand here
      Set(sand, State.Sand);

      // We are not in freefall
      return false;
    }

    public bool IsSpoutSandy() => _grid.ContainsKey((500, 0)) && _grid[(500, 0)] == State.Sand;

    public bool IsTooTall() => _grid.Min(kv => kv.Key.y) < 0;

    public void DropWithFloor(int x = 500, int y = 0) => DropWithFloor((x, y));
    public void DropWithFloor((int x, int y) sand) {
      /* Console.WriteLine($"Dropping from ({sand.x}, {sand.y})..."); */
      var oldY = sand.y;
      if (!_grid.Keys.Any(p => p.x == sand.x && p.y > sand.y && !IsAir(p))) {
        // We are in freefall; however, just add to the floor
        /* Console.WriteLine("*************************Freefall"); */
        Set(sand.x, _floorY, State.Rock);
      }

      // Get highest non-air (State.Air or not in grid) point on the same x-axis
      sand = _grid
        .Where(p => p.Key.x == sand.x && p.Key.y > sand.y && !IsAir(p.Key))
        .OrderBy(p => p.Key.y)
        .First()
        .Key;

      // Step backwards once
      sand = (sand.x, sand.y - 1);
      /* Console.WriteLine($"Fell {sand.y - oldY}..."); */

      /* Console.WriteLine($"Attempting to settle at ({sand.x}, {sand.y})..."); */

      // Check left diagonal
      if (IsAir(sand.x - 1, sand.y + 1)) {
        // Was that supposed to be floor?
        if (sand.y + 1 == _floorY) {
          /* Console.WriteLine("Expand floor left..."); */
          Set(sand.x - 1, _floorY, State.Rock);
        } else {
          /* Console.WriteLine("Redrop left..."); */
          DropWithFloor(sand.x - 1, sand.y + 1);
          return;
        }
      }

      // Check right diagonal
      if (IsAir(sand.x + 1, sand.y + 1)) {
        // Was that supposed to be floor?
        if (sand.y + 1 == _floorY) {
          /* Console.WriteLine("Expand floor right..."); */
          Set(sand.x + 1, _floorY, State.Rock);
        } else {
          /* Console.WriteLine("Redrop right..."); */
          DropWithFloor(sand.x + 1, sand.y + 1);
          return;
        }
      }

      // Neither diagonals are open, so settle the sand here
      /* Console.WriteLine("Settled"); */
      Set(sand, State.Sand);
    }

    public override string ToString() {
      // Get bounds
      var minX = _grid.Keys.Min(p => p.x);
      var maxX = _grid.Keys.Max(p => p.x);
      var minY = _grid.Keys.Min(p => p.y);
      var maxY = _grid.Keys.Max(p => p.y);

      var sb = new StringBuilder();

      for (var y = minY; y <= maxY; y++) {
        for (var x = minX; x <= maxX; x++) {
          var p = (x, y);

          if (_grid.ContainsKey(p)) {
            sb.Append(_grid[p] switch {
              State.Rock => "#",
              State.Sand => "o",
              State.Start => "S",
              _ => "."
            });
          } else {
            sb.Append(".");
          }
        }

        sb.Append("\n");
      }

      return sb.ToString();
    }

    public static Cave GenerateCaveFromInput(string input) {
      var cave = new Cave();

      var lines = input
        .ToLines()
        .Select(l => l
          .Split(" -> ")
          .Select(p => p.Split(",").Select(int.Parse).ToList())
          .Select(a => (x: a[0], y: a[1]))
          .ToList());

      foreach (var l in lines) {
        for (var i = 0; i < l.Count() - 1; i++) {
          cave.DrawLine(l[i], l[i + 1]);
        }
      }

      return cave;
    }
  }

  public void PartOne(string input) {
    var cave = Cave.GenerateCaveFromInput(input);
    cave.SetFloor();

    while (!cave.Drop()) ;

    Console.WriteLine(cave.SandCount);

    /* var count = 0; */
    /* while (!cave.IsSpoutSandy()) { */
    /*   cave.DropWithFloor(); */
    /*   count++; */

    /*   Console.WriteLine(cave); */
    /* } */

    /* Console.WriteLine(count); */
  }

  public void PartTwo(string input) {
    var cave = Cave.GenerateCaveFromInput(input);
    cave.SetFloor();

    while (!cave.IsSpoutSandy()) {
    /* for (var i = 0; i < 100; i++) { */
      cave.DropWithFloor();

      /* Console.WriteLine("====="); */
      /* Console.WriteLine(cave); */
      if (cave.IsTooTall()) {
        Console.WriteLine(cave);
        throw new Exception("Huh!?");
      };
    }

    Console.WriteLine(cave.SandCount);
  }
}
