
var text = File.ReadAllText("input/day1.txt");

var elfs = text
    .Split("\n")
    .Select(l => string.IsNullOrWhiteSpace(l) ? (int?)null : int.Parse(l))
    .Aggregate(
        (
            Total: new List<IEnumerable<int>>().AsEnumerable(),
            Running: new List<int>().AsEnumerable()
        ),
        (a, b) => b.HasValue
            ? (a.Total, a.Running.Append(b.Value))
            : (a.Total.Append(a.Running), new List<int>().AsEnumerable()),
        a => a.Total.Append(a.Running))
    .Select(e => e.Sum());

var result_1 = elfs
    .Max();

var result_2 = elfs
    .OrderByDescending(s => s)
    .Take(3)
    .Sum();

Console.WriteLine($"Day 1 answers: {result_1}, {result_2}");
