internal partial class Solution
{
    public static DailySolution Day1 = lines =>
    {
        var elfs = lines
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

        return (
            elfs
                .Max(),
            elfs
                .OrderByDescending(s => s)
                .Take(3)
                .Sum()
        );
    };
}