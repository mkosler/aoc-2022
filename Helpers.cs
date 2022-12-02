using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;

namespace aoc;

public static class Helpers {
  public static string[] ToLines(this string input) => input.Split('\n');

  public static string ReadStdin() {
    var sb = new StringBuilder();
    string? line;

    while ((line = Console.ReadLine()) != null) sb.AppendLine(line);

    return sb.ToString().TrimEnd('\n');
  }

  public static void Deconstruct<T>(this T[] srcArray, out T a0, out T a1) {
    if (srcArray == null || srcArray.Length < 2)
      throw new ArgumentException(nameof(srcArray));

    a0 = srcArray[0];
    a1 = srcArray[0];
  }

  public static void Deconstruct<T>(this T[] srcArray, out T a0, out T a1, out T a2) {
    if (srcArray == null || srcArray.Length < 2)
      throw new ArgumentException(nameof(srcArray));

    a0 = srcArray[0];
    a1 = srcArray[1];
    a2 = srcArray[2];
  }

  public static async Task<string?> GetConfig(string filepath, string key) {
    var config = (await File.ReadAllTextAsync(filepath)).ToLines().Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Split('=')).ToDictionary(x => x[0], x => x[1]);

    return config.ContainsKey(key) ? config[key] : null;
  }

  public static async Task<string?> GetPersonalInput(int year, int day, string session) {
    var uri = new Uri($"https://adventofcode.com");

    var cookieContainer = new CookieContainer();

    using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };

    using var client = new HttpClient(handler) { BaseAddress = uri };

    cookieContainer.Add(uri, new Cookie("session", session));

    return await client.GetStringAsync($"/{year}/day/{day}/input");
  }

  public static string GenerateClass(int year, int day) {
    return $@"
using System.Linq;

namespace aoc.Year{year};

public class Day{day} : IDay {{
  public void PartOne(string input) {{
  }}

  public void PartTwo(string input) {{
  }}
}}";
  }
}
