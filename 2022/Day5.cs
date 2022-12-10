using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aoc.Year2022;

public class Day5 : IDay {
  private readonly Regex _r = new Regex(@"move (\d+) from (\d+) to (\d+)");

  public void PartOne(string input) {
    var (initial, instr) = input.Split("\n\n");

    var stacks = GenerateStacks(initial);

    foreach (var l in instr.ToLines()) {
      var m = _r.Match(l);

      var count = int.Parse(m.Groups[1].Value);
      var start = int.Parse(m.Groups[2].Value) - 1;
      var end = int.Parse(m.Groups[3].Value) - 1;

      var startStack = stacks[start];
      var endStack = stacks[end];

      for (var i = 0; i < count; i++) {
        endStack.Push(startStack.Pop());
      }
    }

    Console.WriteLine(string.Join("", stacks.Select(s => s.Peek())));
  }

  public void PartTwo(string input) {
    var (initial, instr) = input.Split("\n\n");

    var stacks = GenerateStacks(initial);

    foreach (var l in instr.ToLines()) {
      var m = _r.Match(l);

      var count = int.Parse(m.Groups[1].Value);
      var start = int.Parse(m.Groups[2].Value) - 1;
      var end = int.Parse(m.Groups[3].Value) - 1;

      var startStack = stacks[start];
      var endStack = stacks[end];

      var temp = new Stack<string>();

      for (var i = 0; i < count; i++) {
        temp.Push(startStack.Pop());
      }

      while (temp.Count > 0) {
        endStack.Push(temp.Pop());
      }
    }

    Console.WriteLine(string.Join("", stacks.Select(s => s.Peek())));
  }

  private static List<Stack<string>> GenerateStacks(string initial) {
    var lines = Enumerable.Reverse(initial.ToLines()).ToArray();
    var columnCount = (int)char.GetNumericValue(
        lines.First().Where(char.IsDigit).Last());

    var stacks = Enumerable.Range(1, columnCount)
      .Select(x => new Stack<string>())
      .ToList();

    foreach (var l in lines[1..]) {
      for (var i = 0; i < l.Length; i += 4) {
        var maybe = l[(i)..(i + 3)];

        if (!string.IsNullOrWhiteSpace(maybe)) {
          int j = i / 4;

          stacks[j].Push(string.Join("", maybe.Where(char.IsLetter)));
        }
      }
    }

    return stacks;
  }

  private static string Transpose(string s, bool reverse = false) {
    return string.Join("\n", s.ToLines()
      .SelectMany(x => x.Select((ch, i) => new { ch, i }))
      .GroupBy(x => x.i, x => x.ch)
      .Select(x => string.Join("", reverse ? Enumerable.Reverse(x.ToList()) : x.ToList())));
  }
}
