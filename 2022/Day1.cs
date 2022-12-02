using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day1 : IDay {
  public void PartOne(string input) {
    var maxCals = input
      .Split("\n\n")
      .Select(x => x.Split("\n").Select(int.Parse).Aggregate(0, (acc, n) => acc + n))
      .OrderByDescending(x => x)
      .First();

    Console.WriteLine(maxCals);
  }

  public void PartTwo(string input) {
    var topCals = input
      .Split("\n\n")
      .Select(x => x.Split("\n").Select(int.Parse).Aggregate(0, (acc, n) => acc + n))
      .OrderByDescending(x => x)
      .ToArray()[..3].Sum();

    Console.WriteLine(topCals);
  }
}
