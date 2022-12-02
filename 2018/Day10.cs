using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc.Year2018;

public class Day10 : IDay {
  private class Point {
    public (int X, int Y) Position { get; set; }
    public (int X, int Y) Velocity { get; set; }

    public Point(int[] pos, int[] vel) : this(pos[0], pos[1], vel[0], vel[1]) { }
    public Point(int px, int py, int vx, int vy) => (Position, Velocity) = ((px, py), (vx, vy));

    public void Update() {
      Position = (Position.X + Velocity.X, Position.Y + Velocity.Y);
    }

    public void Update(int iter) {
      Position = (Position.X + (Velocity.X * iter), Position.Y + (Velocity.Y * iter));
    }

    public override string ToString() => $"[P({Position.X}, {Position.Y}) V({Velocity.X}, {Velocity.Y})]";
  }

  private readonly Regex _r = new Regex(@"position=<(.+)> velocity=<(.+)>");

  public void PartOne(string input) {
    var points = input
      .ToLines()
      .Select(l => _r.Match(l))
      .Select(m => new Point(
        m.Groups[1].Value.Split(",").Select(int.Parse).ToArray(),
        m.Groups[2].Value.Split(",").Select(int.Parse).ToArray()))
      .ToList();

    var box = GetBoundingBox(points);

    var sec = 0;

    while (Area(box.l, box.t, box.r, box.b) > 1000) {
      foreach (var p in points) p.Update();

      sec++;

      box = GetBoundingBox(points);
    }

    Console.WriteLine(PrintMap(points, box.l, box.t, box.r, box.b));
  }

  public void PartTwo(string input) {
    var points = input
      .ToLines()
      .Select(l => _r.Match(l))
      .Select(m => new Point(
        m.Groups[1].Value.Split(",").Select(int.Parse).ToArray(),
        m.Groups[2].Value.Split(",").Select(int.Parse).ToArray()))
      .ToList();

    var box = GetBoundingBox(points);

    var sec = 0;

    while (Area(box.l, box.t, box.r, box.b) > 1000) {
      foreach (var p in points) p.Update();

      sec++;

      box = GetBoundingBox(points);
    }

    Console.WriteLine(sec);
  }

  private static ulong Area(int l, int t, int r, int b) => (ulong)(Math.Abs(r - l) * Math.Abs(b - t));

  private static (int l, int t, int r, int b) GetBoundingBox(IEnumerable<Point> points) {
    var left = points.OrderBy(p => p.Position.X).First().Position.X;
    var right = points.OrderBy(p => p.Position.X).Last().Position.X;
    var bottom = points.OrderBy(p => p.Position.Y).First().Position.Y;
    var top = points.OrderBy(p => p.Position.Y).Last().Position.Y;

    return (left, top, right, bottom);
  }

  private static string PrintMap(IEnumerable<Point> points, int left, int top, int right, int bottom) {
    var sb = new StringBuilder();

    for (var y = bottom; y <= top; y++) {
      for (var x = left; x <= right; x++) {
        sb.Append(points.Any(p => p.Position == (x, y)) ? "#" : ".");
      }
      sb.AppendLine();
    }

    return sb.ToString();
  }
}
