using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace aoc.Year2022;

public class Day10 : IDay {
  private readonly Dictionary<string, int> _cycleLengths = new Dictionary<string, int> {
    ["noop"] = 1,
    ["addx"] = 2,
  };
  private readonly int[] _specialCycles = new int[] { 20, 60, 100, 140, 180, 220 };
  private const int _width = 40;

  public void PartOne(string input) {
    var prog = GetProg(input);

    var values = _specialCycles
      .Select(x => (Cycle: x, Value: GetRegisterValue(prog, x)))
      .ToList();

    Console.WriteLine(string.Join("\n", values.Select(x => $"{x.Cycle} * {x.Value} = {x.Value * x.Cycle}")));
    Console.WriteLine(values.Aggregate(0, (acc, x) => acc + (x.Value * x.Cycle)));
  }

  public void PartTwo(string input) {
    var prog = GetProg(input).OrderByDescending(x => x.StartCycle).ToList();
    var screen = Enumerable.Repeat(false, 240).ToList();
    var regX = 1;

    for (var c = 1; c <= 240; c++) {
      // Start of cycle
      var current = prog.First(x => x.StartCycle <= c);
      var diff = c - current.StartCycle;
      var instrLength = _cycleLengths[current.Instr[..4]];

      // During cycle
      var pos = c - 1;
      var spriteX = regX + ((int)(pos / _width) * _width);
      screen[pos] = spriteX - 1 <= pos && pos <= spriteX + 1;
      
      // End of cycle
      if (current.Instr.StartsWith("addx") && diff == instrLength - 1) {
        regX += int.Parse(current.Instr.Split(" ")[1]);
      }
    }

    Console.WriteLine(GetScreenString(screen));
  }

  private List<(string Instr, int StartCycle)> GetProg(string input) {
    var prog = new List<(string Instr, int StartCycle)>();
    var cycle = 1;

    foreach (var line in input.ToLines()) {
      prog.Add((Instr: line, StartCycle: cycle));

      cycle += _cycleLengths[line[..4]];
    }

    return prog;
  }

  private static int GetRegisterValue(List<(string Instr, int StartCycle)> prog, int cycle) {
    var relevant = prog
      .Where(x => x.StartCycle <= cycle - 2 && x.Instr.StartsWith("addx"));

    return relevant.Aggregate(1, (acc, x) => acc + int.Parse(x.Instr.Split(" ")[1]));
  }

  private string GetScreenString(List<bool> screen) {
    var sb = new StringBuilder();

    for (var x = 0; x < screen.Count; x++) {
      sb.Append(screen[x] ? "#" : ".");

      if (x % _width == _width - 1) sb.Append("\n");
    }

    return sb.ToString();
  }
}
