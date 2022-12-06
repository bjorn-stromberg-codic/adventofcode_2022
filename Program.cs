{
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
}

{
    var text = File.ReadAllText("input/day2.txt");

    var plays = text
        .Split("\n")
        .Select(l => l.Split())
        .Select(p => (
            They: p[0][0] - 'A',
            Us: p[1][0] - 'X'
        ));

    var result_1 = plays
        .Sum(p => 1 + p.Us + (p.Us - p.They) switch
        {
            2 or -1 => 0,
            0 => 3,
            _ => 6,
        });

    var result_2 = plays
        .Select(p => (
            They: p.They,
            Us: p.Us switch
            {
                0 => (p.They + 2) % 3,
                1 => p.They,
                _ => (p.They + 1) % 3
            }
        ))
        .Sum(p => 1 + p.Us + (p.Us - p.They) switch
        {
            2 or -1 => 0,
            0 => 3,
            _ => 6,
        });

    Console.WriteLine($"Day 2 answers: {result_1}, {result_2}");
}

static class IEnumerableExtensions
{
    public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, int count, Action<T> action)
    {
        foreach (var v in enumerable.Take(count))
            action(v);
        return enumerable;
    }
}