using System.Text;

namespace aoc.Year2018;

public class Day5 : IDay {
  private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

  public void PartOne(string input) {
    string result;
    string current = input;

    while ((result = MergePolymers(current)) != current) current = result;

    Console.WriteLine($"{result}: {result.Length}");
  }

  public void PartTwo(string input) {
    var counts = new Dictionary<char, int>();

    foreach (var ch in alphabet) {
      if (!input.Contains(ch)) continue;

      var filtered = string.Join("", input.Where(x => char.ToLower(x) != ch));

      string result;
      string current = filtered;

      while ((result = MergePolymers(current)) != current) current = result;

      counts[ch] = result.Length;
    }

    var shortest = counts.OrderBy(kv => kv.Value).First();

    Console.WriteLine($"{shortest.Key}: {shortest.Value}");
  }

  private static string MergePolymers(string input) {
    var sb = new StringBuilder();

    for (var i = 0; i < input.Length; i++) {
      if (i == input.Length - 1) sb.Append(input[i]);
      else if (input[i] != input[i + 1] && char.ToLower(input[i]) != char.ToLower(input[i + 1])) sb.Append(input[i]);
      else if (input[i] == input[i + 1]) sb.Append(input[i]);
      else i++;
    }

    return sb.ToString();
  }
}
