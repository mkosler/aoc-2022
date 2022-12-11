using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace aoc.Year2022;

public class Day11 : IDay {
  private class Monkey {
    public int Count { get; private set; }
    public long Divisor { get; init; }

    private readonly Queue<long> _items;
    private readonly Func<long, long> _operation;
    private readonly Func<long, int> _test;
    private readonly Func<long, long> _reduction;

    public Monkey(Func<long, long> operation, Func<long, int> test, Func<long, long> reduction,
        long divisor, IEnumerable<long>? initial = null) {
      _operation = operation;
      _test = test;
      _reduction = reduction;
      Divisor = divisor;
      _items = initial != null ? new Queue<long>(initial) : new Queue<long>();
    }

    public bool HasItems() => _items.Any();

    public void Add(long item) => _items.Enqueue(item);

    public (int Monkey, long Item) Next(long reduce) {
      Count++;

      var item = _reduction(_operation(_items.Dequeue()));

      if (reduce != null) item = item % reduce;

      return (_test(item), item);
    }

    public static Monkey GenerateFromBlock(string mb, Func<long, long> reduction) {
      var lines = mb
        .ToLines()
        .Select(l => l.Trim())
        .ToList();

      // Starting items
      var startingItems = lines[1]
        .Split(": ")[1]
        .Split(", ")
        .Select(long.Parse);

      // Operation
      var body = lines[2].Split(" = ")[1].Split(" ");

      var lhs = Expression.Parameter(typeof(long), body[0]);
      Expression rhs = body[0] == body[2] 
        ? lhs 
        : Expression.Constant(long.Parse(body[2]));

      var operation = Expression.Lambda(
        typeof(Func<long, long>),
        body[1] switch {
          "*" => Expression.Multiply(lhs, rhs),
          "+" => Expression.Add(lhs, rhs),
          _ => throw new ArgumentOutOfRangeException(),
        },
        lhs);

      // Test
      var d = long.Parse(string.Join("", lines[3].Where(char.IsDigit)));
      var tm = int.Parse(string.Join("", lines[4].Where(char.IsDigit)));
      var fm = int.Parse(string.Join("", lines[5].Where(char.IsDigit)));

      var param = Expression.Parameter(typeof(long), "x");

      var test = Expression.Lambda(
        Expression.Condition(
          Expression.Equal(
            Expression.Modulo(
              param,
              Expression.Constant(d, typeof(long))),
            Expression.Constant(0L, typeof(long))),
          Expression.Constant(tm, typeof(int)),
          Expression.Constant(fm, typeof(int))),
        param);

      return new Monkey(
        (Func<long, long>)operation.Compile(),
        (Func<long, int>)test.Compile(),
        reduction,
        d,
        startingItems);
    }
  }

  public void PartOne(string input) {
    var monkeys = GetMonkeys(input, n => n / 3).ToList();

    RunFor(monkeys, 20);

    Console.WriteLine(string.Join("\n",
      monkeys.Select((m, i) => $"Monkey {i} inspected items {m.Count} times.")));

    Console.WriteLine(GetTopTwoMonkeys(monkeys).Aggregate(1, (acc, x) => acc * x.Count));
  }

  public void PartTwo(string input) {
    var monkeys = GetMonkeys(input, n => n).ToList();

    RunFor(monkeys, 10000);

    Console.WriteLine(string.Join("\n",
      monkeys.Select((m, i) => $"Monkey {i} inspected items {m.Count} times.")));

    Console.WriteLine(GetTopTwoMonkeys(monkeys).Aggregate(1L, (acc, x) => acc * (long)x.Count));
  }

  private static IEnumerable<Monkey> GetMonkeys(string input, Func<long, long> reduction) {
    return input
      .Split("\n\n")
      .Select(x => Monkey.GenerateFromBlock(x, reduction));
  }

  private static void RunFor(List<Monkey> monkeys, int rounds) {
    var lcm = monkeys.Aggregate(1L, (acc, x) => acc * x.Divisor);

    for (var r = 0; r < rounds; r++) {
      foreach (var m in monkeys) {
        while (m.HasItems()) {
          var tossed = m.Next(lcm);

          monkeys[tossed.Monkey].Add(tossed.Item);
        }
      }
    }
  }

  private static IEnumerable<Monkey> GetTopTwoMonkeys(IEnumerable<Monkey> monkeys) {
    return monkeys
      .OrderByDescending(x => x.Count)
      .ToArray()[..2];
  }
}
