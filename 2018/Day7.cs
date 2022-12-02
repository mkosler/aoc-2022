/* using System.Text; */
/* using System.Linq; */
/* using System.Collections.Generic; */
/* using System.Text.RegularExpressions; */

/* namespace aoc.Year2018; */

/* public class Day7 : IDay { */
/*   private readonly Regex _r = new Regex(@"Step (\w+) must be finished before step (\w+) can begin\."); */

/*   public void PartOne(string input) { */
/*     var graph = GenerateGraph(input); */

/*     var start = graph.Where(kv => !kv.Value.Any()).Select(kv => kv.Key).ToList(); */

/*     Console.WriteLine(Traversal(graph, start)); */
/*   } */

/*   public void PartTwo(string input) { */
/*     var graph = GenerateGraph(input); */

/*     var time = graph.ToDictionary(kv => kv.Key, kv => (int)(kv.Key[0] - 'A' + 61)); */

/*     var start = graph.Where(kv => !kv.Value.Any()).Select(kv => kv.Key).ToList(); */

/*     Console.WriteLine(MultiWorkerTraversal(graph, time, start, 5)); */
/*   } */

/*   private Dictionary<string, string[]> GenerateGraph(string input) { */
/*     var graph = new Dictionary<string, string[]>(); */

/*     var instr = input.ToLines().Select(l => _r.Match(l)) */
/*       .Select(x => (Before: x.Groups[1].Value, After: x.Groups[2].Value)); */

/*     var keys = new HashSet<string>(instr.Select(x => new string[] { x.Before, x.After }).SelectMany(x => x)); */

/*     foreach (var k in keys) { */
/*       graph[k] = instr.Where(x => x.After == k).Select(x => x.Before).ToArray(); */
/*     } */

/*     return graph; */
/*   } */

/*   private static string Traversal(Dictionary<string, string[]> graph, List<string> available) { */
/*     var discovered = new HashSet<string>(); */
/*     var sb = new StringBuilder(); */

/*     while (available.Any()) { */
/*       var current = available.First(); */
/*       available.RemoveAt(0); */
/*       sb.Append(current); */
/*       discovered.Add(current); */

/*       var edges = graph.Where(kv => kv.Value.Contains(current)); */

/*       available.AddRange(edges.Where(kv => kv.Value.All(x => discovered.Contains(x))).Select(kv => kv.Key)); */

/*       available = available.OrderBy(x => x).ToList(); */
/*     } */

/*     return sb.ToString(); */
/*   } */

/*   private static int MultiWorkerTraversal(Dictionary<string, string[]> edges, Dictionary<string, int> time, List<string> initial, int totalWorkers) { */
/*     var discovered = new HashSet<string>(); */
/*     var totalTime = 0; */
/*     var workers = new PriorityQueue<string, int>(); */
/*     var available = new Queue<string>(initial); */
/*     var totalTasks = edges.Count; */

/*     while (discovered.Count < totalTasks) { */
/*       while (available.Any() && workers.Count < totalWorkers) { */
/*         var current = available.Dequeue(); */
/*         workers.Enqueue(current, time[current]); */
/*       } */

/*       if (workers.Count == totalWorkers || !available.Any()) { */
/*         string current; */
/*         int minTime; */

/*         workers.TryDequeue(out current, out minTime); */

/*         discovered.Add(current); */

/*         totalTime += minTime; */

/*         var newWorkers = new PriorityQueue<string, int>(); */

/*         int t; */
/*         string job; */

/*         while (workers.TryDequeue(out job, out t)) newWorkers.Enqueue(job, t - minTime); */

/*         workers = newWorkers; */

/*         var currentEdges = edges.Where(kv => kv.Value.Contains(current)); */

/*         foreach (var e in currentEdges.Where(kv => kv.Value.All(x => discovered.Contains(x))).Select(kv => kv.Key)) { */
/*           available.Enqueue(e); */
/*         } */

/*         available = new Queue<string>(available.OrderBy(x => x)); */
/*       } */
/*     } */

/*     return totalTime; */
/*   } */
/* } */
