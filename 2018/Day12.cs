using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aoc.Year2018;

public class Day12 : IDay {
  private readonly Regex _r = new Regex(@"(.+) => (.)");

  public void PartOne(string input) {
    var lines = input.ToLines();

    var state = lines[0]
      .Where(c => c == '#' || c == '.')
      .Select((x, i) => (Value: x == '#', Index: i))
      .ToDictionary(x => x.Index, x => x.Value);

    /* Console.WriteLine($"Initial State: {string.Join(", ", state.Select(s => $"({s.Key}, {s.Value})"))}"); */

    var rules = lines[2..]
      .Select(l => _r.Match(l))
      .ToDictionary(m => ConvertToByte(m.Groups[1].Value.ToCharArray().Select(x => x == '#').ToArray()),
          m => m.Groups[2].Value == "#");

    /* foreach (var r in rules) { */
    /*   Console.WriteLine($"{string.Join("", $"{r.Key} -> {r.Value}")}"); */
    /* } */

    for (var i = 0; i <= 20; i++) {
      Console.WriteLine($"{i}: {string.Join("", state.Select(s => s.Value ? "#" : "."))}");

      state = Generate(state, rules);
    }

    Console.WriteLine(string.Join(" ", state.Where(kv => kv.Value).Select(kv => kv.Key)));
    Console.WriteLine(state.Where(kv => kv.Value).Aggregate(0, (acc, kv) => acc + kv.Key));
  }

  public void PartTwo(string input) {
  }

  private static byte ConvertToByte(bool[] arr) {
    var bits = new BitArray(arr);
    byte[] b = new byte[1];
    bits.CopyTo(b, 0);
    return b[0];
  }

  private static bool[] GetTestArray(Dictionary<int, bool> state, int index) {
    var test = new bool[5];

    var j = 0;
    for (var i = index - 2; i <= index + 2; i++) {
      test[j++] = state.ContainsKey(i) && state[i];
    }

    return test;
  }

  private static Dictionary<int, bool> Generate(Dictionary<int, bool> state, Dictionary<byte, bool> rules) {
    var newState = new Dictionary<int, bool>();

    var minKey = state.Keys.OrderBy(x => x).First();
    var maxKey = state.Keys.OrderBy(x => x).Last();

    for (var i = minKey - 2; i <= maxKey + 2; i++) {
      /* var arr = GetTestArray(state, i); */
      /* var test = ConvertToByte(arr); */

      /* Console.WriteLine($"{test} ({i}): {string.Join("", arr.Select(x => x ? "#" : "."))}"); */
      var test = ConvertToByte(GetTestArray(state, i));

      newState[i] = rules.ContainsKey(test) && rules[test];

      /* var result = rules.ContainsKey(test) && rules[test]; */

      /* newState[i] = result; */
    }

    return newState;
  }
}
