using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day4 : IDay {
  public void PartOne(string input) {
    var pairs = GeneratePairs(input);

    var hasSubset = pairs.Where(p =>
      Helpers.IsSubset(p[0].From, p[0].To, p[1].From, p[1].To)
      || Helpers.IsSubset(p[1].From, p[1].To, p[0].From, p[0].To));

    Console.WriteLine(hasSubset.Count());
  }

  public void PartTwo(string input) {
    var pairs = GeneratePairs(input);

    var hasOverlaps = pairs.Where(p => Helpers.Overlaps(p[0].From, p[0].To, p[1].From, p[1].To));

    Console.WriteLine(hasOverlaps.Count());
  }

  private static IEnumerable<List<(int From, int To)>> GeneratePairs(string input) {
    return input
      .ToLines()
      .Select(l => l
        .Split(",")
        .Select(x => x
          .Split("-")
          .Select(int.Parse)
          .ToList())
        .Select(x => (From: x[0], To: x[1]))
        .ToList());
  }
}
