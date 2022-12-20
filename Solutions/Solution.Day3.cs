internal partial class Solution
{
    public static DailySolution Day3 = rugsacks =>
    {
        var priority = (char c) =>
            c switch
            {
                >= 'a' and <= 'z' => (c - 'a') + 1,
                _ => (c - 'A') + 27,
            };

        return (
            rugsacks
                .Select(l => (
                    Left: l.Substring(0, l.Length / 2),
                    Right: l.Substring(l.Length / 2)
                ))
                .Select(r => r.Left.Intersect(r.Right).Single())
                .Sum(priority),
            rugsacks
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
                .Sum(priority));
    };
}
