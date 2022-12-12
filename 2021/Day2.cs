using System;
using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2021;

public class Day2 : IDay {

  public void PartOne(string input) {
    var movements = new Dictionary<string, (int x, int y)> {
      ["forward"] = (1, 0),
      ["down"] = (0, 1),
      ["up"] = (0, -1),
    };

    var pos = input
      .ToLines()
      .Select(l => l.Split(" "))
      .Aggregate((x: 0, y: 0), (pos, x) => {
        var move = movements[x[0]];
        var mag = int.Parse(x[1]);

        return (pos.x + (move.x * mag), pos.y + (move.y * mag));
      });

    Console.WriteLine(pos.x * pos.y);
  }

  public void PartTwo(string input) {
    var pos = input
      .ToLines()
      .Select(l => l.Split(" "))
      .Aggregate((x: 0, y: 0, angle: 0), (pos, x) => {
          var mag = int.Parse(x[1]);

          return x[0] switch {
            "forward" => (pos.x + mag, pos.y + (pos.angle * mag), pos.angle),
            "up" => (pos.x, pos.y, pos.angle - mag),
            "down" => (pos.x, pos.y, pos.angle + mag),
            _ => throw new ArgumentOutOfRangeException(),
          };
      });

    Console.WriteLine(pos.x * pos.y);
  }
}
