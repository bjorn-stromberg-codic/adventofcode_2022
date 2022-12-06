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

    var score = (int us, int they) => 1 + us + (us - they) switch
    {
        2 or -1 => 0,
        0 => 3,
        _ => 6,
    };

    var result_1 = plays
        .Sum(p => score(p.Us, p.They));

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
        .Sum(p => score(p.Us, p.They));

    Console.WriteLine($"Day 2 answers: {result_1}, {result_2}");
}

{
    var text = File.ReadAllText("input/day3.txt");

    var rugsacks = text
        .Split("\n");

    var priority = (char c) => c switch
        {
            >= 'a' and <= 'z' => (c - 'a') + 1,
            _ => (c - 'A') + 27,
        };

    var result_1 = rugsacks
        .Select(l => (
            Left: l.Substring(0, l.Length / 2),
            Right: l.Substring(l.Length / 2)
        ))
        .Select(r => r.Left.Intersect(r.Right).Single())
        .Sum(priority);

    var result_2 = rugsacks
        .Zip(Enumerable.Range(0, rugsacks.Count()).Select(n => n / 3))
        .GroupBy(zip => zip.Second)
        .Select(group => group
            .Select(zip => zip.First.Trim()))
        .Select(group => group
            .Take(2)
            .Aggregate(
                group.Last().AsEnumerable(),
                (a, b) => a.Intersect(b))
            .Single())
        .Sum(priority);

    Console.WriteLine($"Day 3 answers: {result_1}, {result_2}");
}

static class IEnumerableExtensions
{
    public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, int count, Action<T> action)
    {
        foreach (var v in enumerable.Take(count))
            action(v);
        return enumerable;
    }
    public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var v in enumerable)
            action(v);
        return enumerable;
    }
}