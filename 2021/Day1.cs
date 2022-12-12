using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2021;

public class Day1 : IDay {
  public void PartOne(string input) {
    var depths = input
      .ToLines()
      .Select(int.Parse)
      .ToList();

    var count = 0;

    for (var i = 0; i < depths.Count - 1; i++) {
      if (depths[i + 1] - depths[i] > 0) count++;
    }

    Console.WriteLine(count);
  }

  public void PartTwo(string input) {
    var depths = input
      .ToLines()
      .Select(int.Parse)
      .ToArray();

    var count = 0;

    for (var i = 0; i < depths.Count() - 3; i++) {
      var a = depths[i..(i + 3)].Aggregate(0, (acc, x) => acc + x);
      var b = depths[(i + 1)..(i + 4)].Aggregate(0, (acc, x) => acc + x);

      if (b - a > 0) count++;
    }

    Console.WriteLine(count);
  }
}
