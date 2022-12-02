using System;
using System.Linq;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day2 : IDay {
  private enum RPS { Rock, Paper, Scissors }

  public void PartOne(string input) {
    var rounds = input
      .ToLines()
      .Select(l => l.Split(" ").Select(Convert).ToArray());

    var roundScores = rounds.Select(x => MatchResult(x[0], x[1]) + (int)x[1] + 1);

    Console.WriteLine(roundScores.Sum());
  }

  public void PartTwo(string input) {
    var rounds = input
      .ToLines()
      .Select(l => l.Split(" "))
      .Select(x => (Opponent: Convert(x[0]), MatchResult: MatchConvert(x[1])));

    var roundScores = rounds.Select(x => x.MatchResult + (int)GetShape(x.Opponent, x.MatchResult) + 1);

    Console.WriteLine(roundScores.Sum());
  }

  private static RPS Convert(string ch) => ch switch {
    "A" => RPS.Rock,
    "X" => RPS.Rock,
    "B" => RPS.Paper,
    "Y" => RPS.Paper,
    "C" => RPS.Scissors,
    "Z" => RPS.Scissors,
    _ => throw new ArgumentOutOfRangeException(nameof(ch)),
  };

  private static int MatchResult(RPS opponent, RPS you) => ((int)you - (int)opponent) switch {
    0 => 3,
    1 => 6,
    -2 => 6,
    _ => 0
  };

  private static int MatchConvert(string ch) => ch switch {
    "X" => 0,
    "Y" => 3,
    "Z" => 6,
    _ => throw new ArgumentOutOfRangeException(nameof(ch)),
  };

  private static RPS GetShape(RPS opponent, int result) {
    if (result == 3) return opponent;
    else if (result == 6) return (RPS)(((int)opponent + 1) % 3);
    else {
      var r = (int)opponent - 1 % 3;
      return (RPS)(r < 0 ? r + 3 : r);
    }
  }
}
