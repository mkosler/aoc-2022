using System.Linq;
using System.Text;
using System.Collections.Generic;
using Grid = System.Collections.Generic.Dictionary<(int x, int y), int>;

namespace aoc.Year2018;

public class Day11 : IDay {
  public void PartOne(string input) {
    var serial = int.Parse(input);

    var grid = new Grid();

    for (var x = 1; x <= 300; x++) {
      for (var y = 1; y <= 300; y++) {
        grid[(x, y)] = CalculatePower(serial, x, y);
      }
    }

    var regions = new Grid();

    for (var x = 1; x <= 300; x++) {
      for (var y = 1; y <= 300; y++) {
        regions[(x, y)] = CalculateRegionPower(grid, x, y, 3);
      }
    }

    var maxRegion = regions.GroupBy(x => x.Value).Select(x => new {
      Power = x.Key,
      Regions = x.ToList()
    }).OrderByDescending(x => x.Power).First();

    Console.WriteLine($"{maxRegion.Power}: {string.Join(", ", maxRegion.Regions.Select(r => $"({r.Key.x}, {r.Key.y})"))}");
  }

  public void PartTwo(string input) {
    var serial = int.Parse(input);

    var grid = new Grid();

    for (var x = 1; x <= 300; x++) {
      for (var y = 1; y <= 300; y++) {
        grid[(x, y)] = CalculatePower(serial, x, y);
      }
    }

    var regions = new Dictionary<(int x, int y, int size), int>();

    for (var x = 1; x <= 300; x++) {
      for (var y = 1; y <= 300; y++) {
        var t = x > y ? x : y;
        /* Console.WriteLine($"{x}, {y}, {t}, {300 - (t - 1)}"); */

        for (var s = 1; s <= 300 - (t - 1); s++) {
          var p = CalculateRegionPower(grid, x, y, s);

          if (p < -100) break;

          regions[(x, y, s)] = p;

          /* Console.WriteLine($"{s}: {regions[(x, y, s)]}"); */
        }
      }
    }

    var maxRegion = regions.GroupBy(x => x.Value).Select(x => new {
      Power = x.Key,
      Regions = x.ToList()
    }).OrderByDescending(x => x.Power).First();

    Console.WriteLine($"{maxRegion.Power}: {string.Join(", ", maxRegion.Regions.Select(r => $"({r.Key.x}, {r.Key.y}, {r.Key.size})"))}");
  }

  private static int CalculatePower(int serial, int x, int y) {
    var id = x + 10;
    var p = ((id * y) + serial) * id;
    var h = (int)Math.Abs(p / 100 % 10);
    return h - 5;
  }

  private static int CalculateRegionPower(Grid grid, int x, int y, int size) {
    var p = 0;

    if (x > 300 - size || y > 300 - size) return -1;

    for (var i = x; i < x + size; i++) {
      for (var j = y; j < y + size; j++) {
        p += grid[(i, j)];
      }
    }

    return p;
  }

  private static string PrintGrid(Grid grid) {
    var sb = new StringBuilder();

    for (var y = 1; y <= 300; y++) {
      for (var x = 1; x <= 300; x++) {
        sb.Append(grid[(x, y)].ToString().PadLeft(3));
      }
      sb.AppendLine();
    }

    return sb.ToString();
  }
}
