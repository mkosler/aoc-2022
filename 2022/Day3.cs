using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day3 : IDay {
  public void PartOne(string input) {
    var sacks = GenerateSacks(input);

    var common = sacks.Select(s => string.Join("", s.Left.Intersect(s.Right)));

    Console.WriteLine(common.Aggregate(0, (acc, x) => acc + GetPriority(x[0])));
  }

  public void PartTwo(string input) {
    var sacks = GenerateSacks(input).Select(s => s.Left + s.Right).ToList();

    var groups = sacks.Chunk(3);

    var common = groups
      .Select(g => g.Aggregate(g[0], (acc, x) => string.Join("", acc.Intersect(x))));

    Console.WriteLine(common.Aggregate(0, (acc, x) => acc + GetPriority(x[0])));
  }

  private static IEnumerable<(string Left, string Right)> GenerateSacks(string input) {
    return input
      .ToLines()
      .Select(l => (Left: l[..((int)(l.Length / 2))], Right: l[((int)(l.Length / 2))..]));
  }

  private static int GetPriority(char ch) {
    if (char.IsUpper(ch)) return (int)(ch - 'A') + 27;
    else return (int)(ch - 'a') + 1;
  }
}
