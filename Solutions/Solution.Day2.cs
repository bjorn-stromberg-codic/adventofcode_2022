internal partial class Solution
{
    public static Day Day2 = lines =>
    {
        var plays = lines
            .Select(l => l.Split())
            .Select(p => (
                They: p[0][0] - 'A',
                Us: p[1][0] - 'X'
            ));

        var score = (int us, int they) =>
            1 + us + (us - they) switch
            {
                2 or -1 => 0,
                0 => 3,
                _ => 6,
            };

        return (
            plays
                .Sum(p => score(p.Us, p.They)),
            plays
                .Select(p => (
                    They: p.They,
                    Us: p.Us switch
                    {
                        0 => (p.They + 2) % 3,
                        1 => p.They,
                        _ => (p.They + 1) % 3
                    }
                ))
                .Sum(p => score(p.Us, p.They)));
    };
}