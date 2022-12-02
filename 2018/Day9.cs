/* using System.Linq; */
/* using System.Collections.Generic; */
/* using System.Text.RegularExpressions; */

/* namespace aoc.Year2018; */

/* public class Day9 : IDay { */
/*   private class Node { */
/*     public long Value { get; init; } */
/*     public Node? Prev { get; set; } */
/*     public Node? Next { get; set; } */
/*   } */

/*   private readonly Regex _r = new Regex(@"(\d+) players; last marble is worth (\d+) points"); */

/*   public void PartOne(string input) { */
/*     var m = _r.Match(input); */

/*     var players = int.Parse(m.Groups[1].Value); */
/*     var count = long.Parse(m.Groups[2].Value); */

/*     var scores = Enumerable.Range(0, players).ToDictionary(x => x, x => 0L); */

/*     Node current = new Node { Value = 0 }; */
/*     current.Prev = current; */
/*     current.Next = current; */

/*     for (long i = 1; i <= count; i++) { */
/*       if (i % 23 != 0) { */
/*         var prev = current.Next; */
/*         var next = current.Next.Next; */

/*         current = new Node { Value = i, Prev = prev, Next = next }; */

/*         prev.Next = current; */
/*         next.Prev = current; */
/*       } else { */
/*         var removed = current.Prev.Prev.Prev.Prev.Prev.Prev.Prev; */

/*         scores[(int)(i % players)] += i + removed.Value; */

/*         removed.Prev.Next = removed.Next; */
/*         removed.Next.Prev = removed.Prev; */

/*         current = removed.Next; */

/*         removed.Next = null; */
/*         removed.Prev = null; */
/*       } */
/*     } */

/*     Console.WriteLine(scores.OrderByDescending(kv => kv.Value).First().Value); */
/*   } */

/*   public void PartTwo(string input) { */
/*     var m = _r.Match(input); */

/*     var players = int.Parse(m.Groups[1].Value); */
/*     var count = long.Parse(m.Groups[2].Value) * 100; */

/*     var scores = Enumerable.Range(0, players).ToDictionary(x => x, x => 0L); */

/*     Node current = new Node { Value = 0 }; */
/*     current.Prev = current; */
/*     current.Next = current; */

/*     for (long i = 1; i <= count; i++) { */
/*       if (i % 23 != 0) { */
/*         var prev = current.Next; */
/*         var next = current.Next.Next; */

/*         current = new Node { Value = i, Prev = prev, Next = next }; */

/*         prev.Next = current; */
/*         next.Prev = current; */
/*       } else { */
/*         var removed = current.Prev.Prev.Prev.Prev.Prev.Prev.Prev; */

/*         scores[(int)(i % players)] += i + removed.Value; */

/*         removed.Prev.Next = removed.Next; */
/*         removed.Next.Prev = removed.Prev; */

/*         current = removed.Next; */

/*         removed.Next = null; */
/*         removed.Prev = null; */
/*       } */
/*     } */

/*     Console.WriteLine(scores.OrderByDescending(kv => kv.Value).First().Value); */
/*   } */
/* } */
