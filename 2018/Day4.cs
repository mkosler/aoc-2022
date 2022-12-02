using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aoc.Year2018;

public class Day4 : IDay {
  private enum GuardState { Wakes, Sleeps, Starts }

  private class Guard {
    public int ID { get; init; }
    public readonly List<(int From, int To)> AsleepPeriods = new List<(int From, int To)>();
  }

  private readonly Regex _r = new Regex(@"\[(.+)\] (.+)");
  private const string DATETIME_PATTERN = "yyyy-MM-dd HH:mm";

  public void PartOne(string input) {
    var mostAsleep = GenerateGuards(input).Values
      .OrderByDescending(x => x.AsleepPeriods.Aggregate(0, (acc, x) => acc + (x.To - x.From)))
      .First();

    var everyMinute = new List<int>();

    foreach (var period in mostAsleep.AsleepPeriods) {
      everyMinute.AddRange(Enumerable.Range(period.From, period.To - period.From));
    }

    var mostOccuredMinute = everyMinute.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;

    Console.WriteLine($"ID: {mostAsleep.ID}, Minute: {mostOccuredMinute} = {mostAsleep.ID * mostOccuredMinute}");
  }

  public void PartTwo(string input) {
    var guards = GenerateGuards(input);

    var mostFrequentMinutes = new List<(int ID, int Minute, int Frequency)>();

    foreach (var g in guards.Values) {
      Console.WriteLine($"{g.ID}: {string.Join(" ", g.AsleepPeriods.Select(x => $"[{x.From}, {x.To}]"))}");

      var everyMinute = g.AsleepPeriods
        .Select(x => Enumerable.Range(x.From, x.To - x.From))
        .SelectMany(x => x);

      if (everyMinute != null && everyMinute.Any()) {
        mostFrequentMinutes.Add(everyMinute
          .GroupBy(x => x)
          .Select(x => (ID: g.ID, Minute: x.Key, Frequency: x.Count()))
          .OrderByDescending(x => x.Frequency)
          .First());
      }
    }

    var mostFrequentGuard = mostFrequentMinutes.OrderByDescending(x => x.Frequency).First();

    Console.WriteLine($"ID: {mostFrequentGuard.ID}, Minute: {mostFrequentGuard.Minute} = {mostFrequentGuard.ID * mostFrequentGuard.Minute}");
  }

  private IDictionary<int, Guard>  GenerateGuards(string input) {
    var guards = new Dictionary<int, Guard>();

    var sortedEntries = input
      .ToLines()
      .Select(x => _r.Match(x))
      .Select(x => (Time: DateTime.ParseExact(x.Groups[1].Value, DATETIME_PATTERN, null), Remaining: x.Groups[2].Value))
      .OrderBy(x => x.Time);

    Guard? current = null;
    int? currentFrom = null;

    foreach (var e in sortedEntries) {
      var state = ConvertToState(e.Remaining);

      if (state == GuardState.Starts) {
        var id = int.Parse(string.Join("", e.Remaining.Where(char.IsDigit)));

        current = guards.ContainsKey(id) ? guards[id] : new Guard { ID = id };
      }

      if (current is null) throw new Exception("First entry is not GuardState.Starts. Bad input");

      if (state == GuardState.Sleeps) currentFrom = e.Time.Minute;
      else if (state == GuardState.Wakes) {
        if (currentFrom is null) throw new Exception("Guard woke up while not asleep");

        current.AsleepPeriods.Add((From: (int)currentFrom, To: e.Time.Minute));
      }

      guards[current.ID] = current;
    }

    return guards;
  }

  private static GuardState ConvertToState(string state) => state switch {
    "wakes up" => GuardState.Wakes,
    "falls asleep" => GuardState.Sleeps,
    _ => GuardState.Starts,
  };
}
