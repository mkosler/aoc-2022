using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace aoc.Year2022;

public class Day12 : IDay {
  private class Node {
    public const char MIN_LETTER = 'a';
    public char Letter { get; init; } = MIN_LETTER;
    public int Height { get; init; } = 0;
    public (int x, int y) Position { get; init; } = (0, 0);
    public readonly List<(int x, int y)> Adjacents = new List<(int x, int y)>();

    public Node(char letter, int x, int y) {
      Letter = letter;
      var test = letter switch {
        'S' => 'a',
        'E' => 'z',
        _ => letter,
      };
      Height = (int)(test - MIN_LETTER);
      Position = (x, y);
    }
  }

  public void PartOne(string input) {
    var graph = new Dictionary<(int x, int y), Node>();

    var lines = input.ToLines();
    var width = lines[0].Length;
    var height = lines.Count();

    for (var y = 0; y < lines.Count(); y++) {
      var l = lines[y];

      for (var x = 0; x < l.Length; x++) {
        var ch = l[x];
        var pos = (x, y);

        graph[pos] = new Node(ch, x, y);
      }
    }

    foreach (var node in graph.Values) {
      var pos = node.Position;

      if (pos.x > 0) {
        var left = (pos.x - 1, pos.y);

        if (graph[left].Height <= node.Height + 1) node.Adjacents.Add(left);
      }

      if (pos.x < width - 1) {
        var right = (pos.x + 1, pos.y);

        if (graph[right].Height <= node.Height + 1) node.Adjacents.Add(right);
      }

      if (pos.y > 0) {
        var up = (pos.x, pos.y - 1);

        if (graph[up].Height <= node.Height + 1) node.Adjacents.Add(up);
      }

      if (pos.y < height - 1) {
        var down = (pos.x, pos.y + 1);

        if (graph[down].Height <= node.Height + 1) node.Adjacents.Add(down);
      }
    }

    var start = graph.Values.Single(x => x.Letter == 'S');
    var finish = graph.Values.Single(x => x.Letter == 'E');
    var path = Dijkstra(graph, start.Position.x, start.Position.y, finish.Position.x, finish.Position.y);

    Console.WriteLine($"{string.Join("", path.Select(p => p.Letter))}: {path.Count() - 1}");
  }

  public void PartTwo(string input) {
    var graph = new Dictionary<(int x, int y), Node>();

    var lines = input.ToLines();
    var width = lines[0].Length;
    var height = lines.Count();

    for (var y = 0; y < lines.Count(); y++) {
      var l = lines[y];

      for (var x = 0; x < l.Length; x++) {
        var ch = l[x];
        var pos = (x, y);

        graph[pos] = new Node(ch, x, y);
      }
    }

    foreach (var node in graph.Values) {
      var pos = node.Position;

      if (pos.x > 0) {
        var left = (pos.x - 1, pos.y);

        if (graph[left].Height <= node.Height + 1) node.Adjacents.Add(left);
      }

      if (pos.x < width - 1) {
        var right = (pos.x + 1, pos.y);

        if (graph[right].Height <= node.Height + 1) node.Adjacents.Add(right);
      }

      if (pos.y > 0) {
        var up = (pos.x, pos.y - 1);

        if (graph[up].Height <= node.Height + 1) node.Adjacents.Add(up);
      }

      if (pos.y < height - 1) {
        var down = (pos.x, pos.y + 1);

        if (graph[down].Height <= node.Height + 1) node.Adjacents.Add(down);
      }
    }

    var finish = graph.Values.Single(x => x.Letter == 'E');
    var allNonEmptyPaths = graph
      .Values
      .Where(n => n.Height == 0)
      .Select(start => Dijkstra(graph, start.Position.x, start.Position.y, finish.Position.x, finish.Position.y))
      .Where(p => p.Count() > 0);

    Console.WriteLine(allNonEmptyPaths.OrderBy(p => p.Count()).First().Count() - 1);
  }

  private static IEnumerable<Node> Dijkstra(Dictionary<(int x, int y), Node> graph, int sx, int sy, int ex, int ey) {
    Console.WriteLine($"({sx}, {sy})");
    var finish = (ex, ey);
    var current = graph[(sx, sy)];
    var visited = new HashSet<(int x, int y)>();
    var dist = new Dictionary<(int x, int y), int>();
    var parent = new Dictionary<(int x, int y), (int x, int y)>();
    dist[current.Position] = 0;

    while (visited.Count() < graph.Count()) {
      var unvisitedNeighbors = current.Adjacents.Except(visited);

      foreach (var n in unvisitedNeighbors) {
        var td = dist[current.Position] + 1;

        if (!dist.ContainsKey(n) || dist[n] > td) {
          dist[n] = td;
          parent[n] = current.Position;
        }
      }

      visited.Add(current.Position);

      if (current.Position == finish) {
        var path = new List<Node>();

        while (parent.ContainsKey(current.Position)) {
          path.Add(current);

          current = graph[parent[current.Position]];
        }

        path.Add(current);

        return Enumerable.Reverse(path);
      }
      
      var unvisited = dist.Where(kv => !visited.Contains(kv.Key));

      if (!unvisited.Any()) break;

      current = graph[unvisited.OrderBy(kv => kv.Value).First().Key];
      /* current = graph[dist */
      /*   .Where(kv => !visited.Contains(kv.Key)) */
      /*   .OrderBy(kv => kv.Value) */
      /*   .First().Key]; */
    }

    Console.WriteLine("Couldn't reach end!");
    return new List<Node>();
  }

  private static string GraphString(Dictionary<(int x, int y), Node> graph, IEnumerable<Node> path, int width, int height) {
    var sb = new StringBuilder();

    for (var y = 0; y < height; y++) {
      for (var x = 0; x < width; x++) {
        var pos = (x, y);

        sb.Append(path.Any(x => x.Position == pos && x.Letter != 'S' && x.Letter != 'E') ? '.' : graph[pos].Letter);
      }

      sb.Append("\n");
    }

    return sb.ToString();
  }
}
