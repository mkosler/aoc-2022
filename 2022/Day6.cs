using System.Linq;

namespace aoc.Year2022;

public class Day6 : IDay {
  public void PartOne(string input) {
    Console.WriteLine(FindFirstStartOfPacket(input, 4));
  }

  public void PartTwo(string input) {
    Console.WriteLine(FindFirstStartOfPacket(input, 14));
  }

  private static int? FindFirstStartOfPacket(string stream, int length) {
    for (var i = 0; i < stream.Length - length; i++) {
      var maybe = stream[(i)..(i + length)];

      if (maybe.Length == maybe.Distinct().Count()) return i + length;
    }

    return null;
  }
}
