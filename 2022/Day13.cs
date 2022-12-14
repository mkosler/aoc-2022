/* using System; */
/* using System.Linq; */
/* using System.Collections.Generic; */
/* using System.Text; */
/* using System.Threading; */

/* namespace aoc.Year2022; */

/* public class Day13 : IDay { */
/*   private class Node { */
/*     public object? Value { get; init; } */
/*     public Node? Parent { get; init; } */
/*     private readonly List<Node> _children = new List<Node>(); */

/*     public Node(Node? parent, object? v = null) { */
/*       Value = v; */
/*       Parent = parent; */
/*     } */

/*     public bool IsLeaf() => _children.Any(); */
/*     public Node? GetChild(int i) => i < _children.Count() ? _children[i] : null; */
/*     public void Add(Node n) => _children.Add(n); */

/*     public override string ToString() { */
/*       if (!_children.Any() && Value != null) return $"{Value}"; */
/*       else if (!_children.Any() && Value == null) return "[]"; */
/*       else return $"[{string.Join(",", _children.Select(x => x.ToString()))}]"; */
/*     } */

/*     public static Node GenerateTree(string input, Node parent = null) { */
/*       if (input == "[]") { */
/*         return new Node(parent); */
/*       } else if (input.All(char.IsDigit)) { */
/*         return new Node(parent, int.Parse(input)); */
/*       } else { */
/*         var node = new Node(parent); */

/*         input = input[1..^1]; */

/*         var substrings = new List<string>(); */
/*         var depth = 0; */
/*         var sb = new StringBuilder(); */

/*         for (var i = 0; i < input.Length; i++) { */
/*           var ch = input[i]; */

/*           if (ch == ',' && depth == 0) { */
/*             substrings.Add(sb.ToString()); */
/*             sb.Clear(); */
/*           } else { */
/*             sb.Append(ch); */

/*             if (ch == '[') depth++; */
/*             if (ch == ']') depth--; */
/*           } */
/*         } */

/*         substrings.Add(sb.ToString()); */

/*         foreach (var s in substrings) { */
/*           node._children.Add(GenerateTree(s, node)); */
/*         } */

/*         return node; */
/*       } */
/*     } */
/*   } */

/*   public void PartOne(string input) { */
/*     var pairs = input */
/*       .Split("\n\n") */
/*       .Select(p => p.ToLines().Select(x => Node.GenerateTree(x)).ToList()) */
/*       .ToList(); */

/*     var inOrder = new List<int>(); */

/*     for (var i = 0; i < pairs.Count(); i++) { */
/*       var p = pairs[i]; */

/*       var (left, right) = (p[0], p[1]); */
/*       var n = 0; */

/*       while (true) { */
/*       } */
/*     } */
/*   } */

/*   public void PartTwo(string input) { */
/*     Console.WriteLine("Part Two"); */
/*   } */

/*   private static int Compare(Node left, Node right, int n = 0) { */
/*     if (!left.IsLeaf() && !right.IsLeaf()) { */
/*       var result = Compare(left.GetChild(n), right.GetChild(n), n); */

/*       if (result == 0) return Compare(left.GetChild(n + 1), right.GetChild(n + 1), n + 1); */
/*       else return result; */
/*     } */

/*     if (left.IsLeaf() && right.IsLeaf()) { */
/*       var li = (int)left.Value; */
/*       var ri = (int)right.Value; */

/*       return ri - li; */
/*     } */

/*     return 0; */

/*     /1* var lc = left.GetChild(n); *1/ */
/*     /1* var rc = right.GetChild(n); *1/ */

/*     /1* if (lc != null && rc == null) return true; *1/ */

/*     /1* if (lc == null && rc != null) return false; *1/ */

/*     /1* if (!lc.IsLeaf() && !rc.IsLeaf()) { *1/ */
/*     /1*   return Compare(lc, rc, 0); *1/ */
/*     /1* } else if (lc.IsLeaf()) { *1/ */
/*     /1*   var node = new Node(null); *1/ */
/*     /1*   node.Add(lc); *1/ */
/*     /1*   return Compare(node, right, 0); *1/ */
/*     /1* } else if (lc.Value is int li && rc.Value is int ri) { *1/ */
/*     /1*   if (li < ri) { *1/ */
/*     /1*     return true; *1/ */
/*     /1*   } else if (li > ri) { *1/ */
/*     /1*     return false; *1/ */
/*     /1*   } else { *1/ */
/*     /1*     return Compare(left, right, n + 1); *1/ */
/*     /1*   } *1/ */
/*     /1* } *1/ */
/*   } */
/* } */
