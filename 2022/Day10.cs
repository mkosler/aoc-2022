using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace aoc.Year2022;

public class Day10 : IDay {
  private class CRT {
    private readonly bool[] _pixels;
    private readonly int _width;

    public CRT(int width, int height) => (_width, _pixels) = (width, new bool[width * height]);

    public void Set(int i, bool b) => _pixels[i] = b;

    public override string ToString() {
      return string.Join("\n",
        _pixels.Chunk(_width).Select(l => string.Join("", l.Select(x => x ? "#" : "."))));
    }
  }

  private class Prog {
    private readonly List<(string Instruction, int StartCycle)> _instructions
      = new List<(string Instruction, int StartCycle)>();

    public static readonly Dictionary<string, int> CycleLengths = new Dictionary<string, int> {
      ["noop"] = 1,
      ["addx"] = 2,
    };

    public int RunFor(int cycles) {
      return _instructions
        .Where(x => x.StartCycle <= cycles - 2)
        .Aggregate(1, (acc, x) => acc + GetValue(x.Instruction));
    }

    public static Prog GenerateFromInput(string input) {
      var prog = new Prog();

      var cycle = 1;

      foreach (var line in input.ToLines()) {
        prog._instructions.Add((line, cycle));

        cycle += Prog.CycleLengths[line[..4]];
      }

      return prog;
    }

    private static int GetValue(string instr) => instr.StartsWith("addx") ? int.Parse(instr.Split(" ")[1]) : 0;
  }

  private readonly int[] _specialCycles = new int[] { 20, 60, 100, 140, 180, 220 };
  private const int _width = 40;
  private const int _height = 6;

  public void PartOne(string input) {
    var prog = Prog.GenerateFromInput(input);

    var values = _specialCycles
      .Select(x => (Cycle: x, Value: prog.RunFor(x)))
      .ToList();

    Console.WriteLine(string.Join("\n", values.Select(x => $"{x.Cycle} * {x.Value} = {x.Value * x.Cycle}")));
    Console.WriteLine(values.Aggregate(0, (acc, x) => acc + (x.Value * x.Cycle)));
  }

  public void PartTwo(string input) {
    var prog = Prog.GenerateFromInput(input);
    var crt = new CRT(_width, _height);

    for (var c = 1; c <= _width * _height; c++) {
      // Start of cycle
      var regX = prog.RunFor(c);

      // During cycle
      var p = c - 1;
      var sprX = regX + ((int)(p / _width) * _width);
      crt.Set(p, sprX - 1 <= p && p <= sprX + 1);
    }

    Console.WriteLine(crt);
  }
}
