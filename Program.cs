using System.IO;
using aoc;

if (args[0] == "init") {
  var result = args[1].Split('.').Select(int.Parse).ToList();

  var (year, day) = (result[0], result[1]);

  var session = await Helpers.GetConfig(".session", "ADVENT_OF_CODE_SESSION");

  if (session == null) throw new Exception("Could not find session key");

  var input = await Helpers.GetPersonalInput(year, day, session);

  var idi = Directory.CreateDirectory($"inputs/{year}/");

  await File.WriteAllTextAsync($"{idi.FullName}/{day}.txt", input);

  var template = Helpers.GenerateClass(year, day);

  var cdi = Directory.CreateDirectory($"{year}/");

  if (!File.Exists($"{cdi.FullName}/Day{day}.cs")) await File.WriteAllTextAsync($"{cdi.FullName}/Day{day}.cs", template);
} else {
  var result = args[0].Split('.').Select(int.Parse).ToList();

  var (year, day, part) = (result[0], result[1], result[2]);

  var type = Type.GetType($"aoc.Year{year}.Day{day}");

  if (type is null) throw new ArgumentException("Could not determine which problem to run");

  var prob = (IDay?)Activator.CreateInstance(type);

  if (prob is null) throw new ArgumentException("Could not determine which problem to run");

  if (part == 1) prob.PartOne(Helpers.ReadStdin());
  else if (part == 2) prob.PartTwo(Helpers.ReadStdin());
  else throw new ArgumentException("Could not determine which problem to run");
}
