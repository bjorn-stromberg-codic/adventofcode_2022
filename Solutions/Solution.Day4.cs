internal partial class Solution
{
    public static Day Day4 = lines =>
    {
        var pairs = lines
            .Select(l => l
                .Split(",")
                .Select(s => s
                    .Split("-")
                    .Select(int.Parse))
                .Select(s => (
                    From: s.First(),
                    To: s.Last()
                )))
            .Select(s =>
            (
                First: s.First(),
                Second: s.Last()
            ));

        var contains = ((int From, int To) a, (int From, int To) b) =>
            a.From <= b.From && b.To <= a.To;

        return (
            pairs
                .Where(p => contains(p.First, p.Second) || contains(p.Second, p.First))
                .Count(),
            pairs
                .Where(p => p.First.From <= p.Second.To && p.First.To >= p.Second.From)
                .Count());
    };
}
