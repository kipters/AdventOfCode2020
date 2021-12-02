using AdventOfCode2020;

internal class DayNine
{
    internal static async Task RunFirst()
    {
        var lines = await File.ReadAllLinesAsync("day9.txt");
        var numbers = lines.Select(long.Parse).ToArray();
        const int preamble = 25;

        var broken = numbers
            .SlidingWindow(preamble + 1)
            .Select(w =>
            {
                var generator = w[0..^1];
                var nut = w[^1];

                var pairs =
                    from a in generator
                    from b in generator
                    select (a, b);

                var match = pairs
                    .Where(p => p.a != p.b)
                    .Any(p => p.a + p.b == nut);

                return (match, nut);
            })
            .First(x => !x.match)
            .nut;

        Console.WriteLine($"First broken number: {broken}");
    }

    internal static async Task RunSecond()
    {
        var lines = await File.ReadAllLinesAsync("day9.txt");
        var numbers = lines.Select(long.Parse).ToArray();
        const int preamble = 25;

        var broken = numbers
            .SlidingWindow(preamble + 1)
            .Select(w =>
            {
                var generator = w[0..^1];
                var nut = w[^1];

                var pairs =
                    from a in generator
                    from b in generator
                    select (a, b);

                var match = pairs
                    .Where(p => p.a != p.b)
                    .Any(p => p.a + p.b == nut);

                return (match, nut);
            })
            .First(x => !x.match)
            .nut;

        var weakness = Enumerable.Range(2, numbers.Length - 1)
            .Select(n => numbers.SlidingWindow(n))
            .SelectMany(_ => _)
            .Where(x => x.Sum() == broken)
            .Select(x => x.Min() + x.Max())
            .First()
            ;

        Console.WriteLine($"The encryption weakness is {weakness}");
    }
}