using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day9 : IDay {
  private class Rope {
    private readonly List<(int x, int y)> _knots = new List<(int x, int y)>();
    private readonly Dictionary<string, (int x, int y)> _directions = new Dictionary<string, (int x, int y)> {
      ["L"] = (-1, 0),
      ["R"] = (1, 0),
      ["D"] = (0, -1),
      ["U"] = (0, 1),
    };
    private readonly HashSet<(int x, int y)> _previousTail = new HashSet<(int x, int y)>();

    public Rope((int x, int y) start, int count) {
      _knots.AddRange(Enumerable.Repeat(start, count));
      _previousTail.Add(start);
    }

    public void Move(string direction, int magnitude) {
      var vec = _directions[direction];

      for (var i = 0; i < magnitude; i++) {
        _knots[0] = (_knots[0].x + vec.x, _knots[0].y + vec.y);

        for (var j = 1; j < _knots.Count; j++) {
          var (curr, prev) = (_knots[j], _knots[j - 1]);

          if (Math.Abs(prev.x - curr.x) > 1 || Math.Abs(prev.y - curr.y) > 1) {
            var dx = prev.x != curr.x
              ? (prev.x - curr.x) / Math.Abs(prev.x - curr.x)
              : 0;
            var dy = prev.y != curr.y
              ? (prev.y - curr.y) / Math.Abs(prev.y - curr.y)
              : 0;

            _knots[j] = (curr.x + dx, curr.y + dy);
          }
        }

        _previousTail.Add(_knots.Last());
      }
    }

    public int VisitedCount() => _previousTail.Count;
    public override string ToString() {
      return string.Join("\n", _previousTail.Select(t => $"({t.x}, {t.y})"));
    }
  }

  public void PartOne(string input) {
    var rope = new Rope((0, 0), 2);

    foreach (var l in input.ToLines()) {
      var m = l.Split(" ");

      rope.Move(m[0], int.Parse(m[1]));
    }

    Console.WriteLine(rope.VisitedCount());
  }

  public void PartTwo(string input) {
    var rope = new Rope((0, 0), 10);

    foreach (var l in input.ToLines()) {
      var m = l.Split(" ");

      rope.Move(m[0], int.Parse(m[1]));
    }

    Console.WriteLine(rope.VisitedCount());
  }
}
