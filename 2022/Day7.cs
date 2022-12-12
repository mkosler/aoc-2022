using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace aoc.Year2022;

public class Day7 : IDay {
  private class Dir {
    public string Name { get; set; } = "";
    public Dir? Parent { get; set; }
    public Dictionary<string, Dir> Children = new Dictionary<string, Dir>();
    public Dictionary<string, int> Files = new Dictionary<string, int>();

    public int GetFilesSize() => Files.Aggregate(0, (acc, x) => acc + x.Value);
    public int GetSize() {
      var size = GetFilesSize();

      foreach (var child in Children) {
        size += child.Value.GetSize();
      }

      return size;
    }
  }

  private const int TOTAL_FILESYSTEM_SIZE = 70000000;
  private const int TOTAL_UPDATE_SIZE = 30000000;

  public void PartOne(string input) {
    var root = GetFilesystem(input);

    var sizes = new List<int>();

    GetAllDirectorySizes(sizes, root);

    Console.WriteLine(sizes.Where(x => x <= 100000).Sum());
  }

  public void PartTwo(string input) {
    var root = GetFilesystem(input);

    var rootSize = root.GetSize();

    var diff = TOTAL_FILESYSTEM_SIZE - rootSize;

    var remainingToUpdate = TOTAL_UPDATE_SIZE - diff;

    var sizes = new List<int>();

    GetAllDirectorySizes(sizes, root);

    Console.WriteLine(sizes.Where(x => x >= remainingToUpdate).OrderBy(x => x).First());
  }

  private static Dir GetFilesystem(string input) {
    var root = new Dir {
      Name = "/",
      Parent = null
    };

    root.Parent = root;

    var current = root;

    foreach (var l in input.ToLines()[1..]) {
      if (l.StartsWith("$ cd")) {
        var name = l[5..];

        if (name == ".." && current.Parent != null) current = current.Parent;
        else if (name == "/") current = root;
        else current = current.Children[name];
      } else if (!l.StartsWith("$")) {
        if (current == null) throw new Exception("Huh?");

        var listing = l.Split(" ");

        if (listing[0] == "dir") {
          if (!current.Children.ContainsKey(listing[1])) {
            current.Children[listing[1]] = new Dir {
              Name = listing[1],
              Parent = current,
            };
          }
        } else {
          var size = int.Parse(listing[0]);
          var name = listing[1];

          current.Files[name] = size;
        }
      }
    }

    return root;
  }

  private static void GetAllDirectorySizes(List<int> sizes, Dir current) {
    sizes.Add(current.GetSize());

    foreach (var child in current.Children) {
      GetAllDirectorySizes(sizes, child.Value);
    }
  }
}
