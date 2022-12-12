using System;
using System.Linq;
using System.Text;

namespace aoc.Year2021;

public class Day3 : IDay {
  public void PartOne(string input) {
    var (gs, es) = input
      .Transpose()
      .ToLines()
      .Select(l => l
        .GroupBy(ch => ch)
        .Select(g => new { N = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count))
      .Aggregate((gamma: "", epsilon: ""), (acc, x) => (acc.gamma + x.First().N, acc.epsilon + x.Last().N));

    Console.WriteLine(Convert.ToInt32(gs, 2) * Convert.ToInt32(es, 2));
  }

  public void PartTwo(string input) {
    Console.WriteLine("Part Two");
  }
}
