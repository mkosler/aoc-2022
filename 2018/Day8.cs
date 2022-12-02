/* using System.Linq; */

/* namespace aoc.Year2018; */

/* public class Day8 : IDay { */
/*   private class Node { */
/*     public int ChildCount { get; init; } */
/*     public int MetadataCount { get; init; } */
/*     public int? Index { get; init; } */
/*     public Node? Parent { get; set; } */
/*     public int[] Metadata = new int[]{ }; */
/*     public override string ToString() => $"({ChildCount} {MetadataCount} | {Index})"; */
/*   } */

/*   public void PartOne(string input) { */
/*     var numbers = input.Split(" ").Select(int.Parse).ToArray(); */

/*     var nodes = new List<Node>(); */

/*     GenerateTree(nodes, numbers); */

/*     Console.WriteLine(nodes.SelectMany(x => x.Metadata).Sum()); */
/*   } */

/*   public void PartTwo(string input) { */
/*     var numbers = input.Split(" ").Select(int.Parse).ToArray(); */

/*     var nodes = new List<Node>(); */

/*     GenerateTree(nodes, numbers); */

/*     Console.WriteLine(GetValue(nodes, nodes.First())); */
/*   } */

/*   private static int[] GenerateTree(List<Node> nodes, int[] numbers, Node? parent = null, int? index = null) { */
/*     var current = new Node { */
/*       ChildCount = numbers[0], */
/*       MetadataCount = numbers[1], */
/*       Parent = parent, */
/*       Index = index, */
/*     }; */
/*     nodes.Add(current); */
/*     numbers = numbers[2..]; */

/*     if (current.ChildCount > 0) { */
/*       for (var i = 0; i < current.ChildCount; i++) { */
/*         numbers = GenerateTree(nodes, numbers, current, i + 1); */
/*       } */
/*     } */

/*     current.Metadata = numbers[..(current.MetadataCount)]; */

/*     return numbers[(current.MetadataCount)..]; */
/*   } */

/*   private static int GetValue(List<Node> nodes, Node current) { */
/*     if (current == null) return 0; */

/*     if (current.ChildCount > 0) { */
/*       var children = nodes.Where(x => x.Parent == current); */

/*       return current.Metadata.Select(x => GetValue(nodes, children.SingleOrDefault(y => y.Index == x))).Sum(); */
/*     } else { */
/*       return current.Metadata.Sum(); */
/*     } */
/*   } */
/* } */
