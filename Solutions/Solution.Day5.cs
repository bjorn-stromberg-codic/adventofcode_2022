internal partial class Solution
{
    public static DailySolution Day5 = lines =>
    {
        var stacks = () => lines
            .TakeWhile(l => !string.IsNullOrWhiteSpace(l))
            .Reverse()
            .Skip(1)
            .Select(l =>
            {
                var matches = Regex.Matches(l, @"\[(?<c>\w)\]| ?(?<c>   )");
                var matchToMaybeChar = (Match match) =>
                    string.IsNullOrWhiteSpace(match.Groups["c"].Value)
                        ? (char?)null :
                        match.Groups["c"].Value[0];
                return matches.Select(matchToMaybeChar).ToList();
            })
            .Aggregate(
                Enumerable.Range(0, 9)
                    .Select(_ => new List<char>())
                    .ToArray(),
                (acc, l) =>
                {
                    for (int i = 0; i < acc.Length; i++)
                    {
                        if (l[i].HasValue)
                            acc[i].Add(l[i].Value);
                    }
                    return acc;
                });

        var instructions = lines
            .SkipWhile(l => !string.IsNullOrWhiteSpace(l))
            .Skip(1)
            .Select(l =>
            {
                var groups = Regex.Match(l, @"move (\d+) from (\d+) to (\d+)").Groups;
                var groupToInt = (Group group) => int.Parse(group.Value);
                return (
                    move: groupToInt(groups[1]),
                    from: groupToInt(groups[2]),
                    to: groupToInt(groups[3])
                );
            });

        List<char>[] CrateMover9000(List<char>[] stacks, (int move, int from, int to) instr)
        {
            var from = stacks[instr.from - 1];
            var to = stacks[instr.to - 1];

            to.AddRange(from.TakeLast(instr.move).Reverse());
            from.RemoveRange(from.Count - instr.move, instr.move);

            return stacks;
        }

        List<char>[] CrateMover9001(List<char>[] stacks, (int move, int from, int to) instr)
        {
            var from = stacks[instr.from - 1];
            var to = stacks[instr.to - 1];

            to.AddRange(from.TakeLast(instr.move));
            from.RemoveRange(from.Count - instr.move, instr.move);

            return stacks;
        }

        return (
            instructions
                .Aggregate(stacks(), CrateMover9000)
                .Select(s => s.Last())
                .AggregateToString(),
            instructions
                .Aggregate(stacks(), CrateMover9001)
                .Select(s => s.Last())
                .AggregateToString());
    };
}
