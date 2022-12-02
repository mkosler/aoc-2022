using System.Linq;
using System.Collections.Generic;
using Grid = System.Collections.Generic.Dictionary<(int x, int y), System.Collections.Generic.List<(int x, int y, int dist)>>;

namespace aoc.Year2018;

public class Day6 : IDay {

  public void PartOne(string input) {
    var points = input
      .ToLines()
      .Select(x => x.Split(", ").Select(int.Parse).ToList())
      .Select(p => (x: p[0], y: p[1]));

    var left = points.Select(p => p.x).Min();
    var right = points.Select(p => p.x).Max();
    var top = points.Select(p => p.y).Min();
    var bottom = points.Select(p => p.y).Max();

    var grid = new Grid();

    for (var x = left; x <= right; x++) {
      for (var y = top; y <= bottom; y++) {
        grid[(x, y)] = points.Select(p => (x: p.x, y: p.y, dist: GetManhattanDistance(x, y, p.x, p.y))).ToList();
      }
    }

    var infinitePoints = new HashSet<(int x, int y)>();
    var areas = new Dictionary<(int x, int y), int>();

    foreach (var kv in grid) {
      var smallestDistPoints = kv.Value
        .GroupBy(x => x.dist)
        .Select(x => new {
          Dist = x.Key,
          Points = x.ToList()
        })
        .OrderBy(x => x.Dist)
        .First();

      if (smallestDistPoints.Points.Count() == 1) {
        var p = smallestDistPoints.Points.First();

        if (kv.Key.x == left || kv.Key.x == right || kv.Key.y == top || kv.Key.y == bottom) {
          infinitePoints.Add((p.x, p.y));
        }

        if (!areas.ContainsKey((p.x, p.y))) areas[(p.x, p.y)] = 0;

        areas[(p.x, p.y)]++;
      }
    }

    var maxArea = areas.Where(kv => !infinitePoints.Contains(kv.Key)).OrderByDescending(kv => kv.Value).First();

    Console.WriteLine(maxArea);
  }

  public void PartTwo(string input) {
    var points = input
      .ToLines()
      .Select(x => x.Split(", ").Select(int.Parse).ToList())
      .Select(p => (x: p[0], y: p[1]));

    var left = points.Select(p => p.x).Min();
    var right = points.Select(p => p.x).Max();
    var top = points.Select(p => p.y).Min();
    var bottom = points.Select(p => p.y).Max();

    var grid = new Grid();

    for (var x = left; x <= right; x++) {
      for (var y = top; y <= bottom; y++) {
        grid[(x, y)] = points.Select(p => (x: p.x, y: p.y, dist: GetManhattanDistance(x, y, p.x, p.y))).ToList();
      }
    }

    var aggregates = grid.Select(kv => kv.Value.Aggregate(0, (acc, x) => acc + x.dist));

    Console.WriteLine($"{aggregates.Count(x => x < 10000)}");

    /* var size = 0; */

    /* foreach (var kv in grid) { */
    /*   var sum = kv.Value.Aggregate(0, (acc, x) => acc + x.dist); */

    /*   if (sum < 32) size++; */
    /* } */
  }

  private static int GetManhattanDistance(int x, int y, int px, int py) => Math.Abs(x - px) + Math.Abs(y - py);
}
