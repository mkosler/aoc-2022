using System.Linq;
using System.Collections.Generic;
using Grid = System.Collections.Generic.Dictionary<(int x, int y), int>;

namespace aoc.Year2022;

public class Day8 : IDay {
  public void PartOne(string input) {
    var grid = ToGrid(input);

    Console.WriteLine(
      grid.Count(kv => IsVisible(grid, kv.Key, (-1, 0))
        || IsVisible(grid, kv.Key, (1, 0))
        || IsVisible(grid, kv.Key, (0, -1))
        || IsVisible(grid, kv.Key, (0, 1))));
  }

  public void PartTwo(string input) {
    var grid = ToGrid(input);

    var scores = grid.Select(kv =>
      ViewScore(grid, kv.Key, (-1, 0))
      * ViewScore(grid, kv.Key, (1, 0))
      * ViewScore(grid, kv.Key, (0, -1))
      * ViewScore(grid, kv.Key, (0, 1)));

    Console.WriteLine(scores.OrderByDescending(x => x).First());
  }

  private static Grid ToGrid(string input) {
    var grid = new Grid();
    var y = 0;

    foreach (var l in input.ToLines()) {
      for (var x = 0; x < l.Length; x++) {
        grid[(x, y)] = (int)char.GetNumericValue(l[x]);
      }

      y++;
    }

    return grid;
  }

  private static bool IsVisible(Grid grid, (int x, int y) point, (int x, int y) vel) {
    var width = grid.OrderByDescending(p => p.Key.x).First().Key.x;
    var height = grid.OrderByDescending(p => p.Key.y).First().Key.y;
    var initial = grid[point];

    while (point.x > 0 && point.x < width && point.y > 0 && point.y < height) {
      point = (point.x + vel.x, point.y + vel.y);

      if (grid[point] >= initial) return false;
    }

    return true;
  }

  private static int ViewScore(Grid grid, (int x, int y) point, (int x, int y) vel) {
    var width = grid.OrderByDescending(p => p.Key.x).First().Key.x;
    var height = grid.OrderByDescending(p => p.Key.y).First().Key.y;
    var initial = grid[point];
    int count = 0;

    while (point.x > 0 && point.x < width && point.y > 0 && point.y < height) {
      count++;
      point = (point.x + vel.x, point.y + vel.y);

      if (grid[point] >= initial) break;
    }

    return count;
  }
}
