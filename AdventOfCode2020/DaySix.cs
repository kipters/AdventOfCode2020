using AdventOfCode2020;

internal class DaySix
{
    internal static async Task RunFirst()
    {
        var lines = await File.ReadAllLinesAsync("day6.txt");

        var forms = lines
            .LineChunks()
            .Select(c => c
                .SelectMany(c => c)
                .Distinct()
                .Count()
            )
            .Sum();

        Console.WriteLine($"Total count: {forms}");
    }

    internal static async Task RunSecond()
    {
        var lines = await File.ReadAllLinesAsync("day6.txt");
        var allLetters = Enumerable
            .Range('a', 26)
            .Select(i => (char)i)
            .ToArray();

        var forms = lines
            .LineChunks()
            .Select(c => c
                .Aggregate(
                    allLetters,
                    (s, l) => s.Intersect(l).ToArray(),
                    s => s.Length
                )
            )
            .Sum();

        Console.WriteLine($"Total count: {forms}");
    }
}