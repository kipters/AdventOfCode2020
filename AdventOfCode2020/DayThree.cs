internal static class DayThree
{
    internal static async Task RunFirst()
    {
        var map = await File.ReadAllLinesAsync("day3.txt");
        var trees = map
            .TraverseForest(3, 1)
            .Sum();

        Console.WriteLine($"{trees} trees in your route");
    }

    internal static async Task RunSecond()
    {
        var map = await File.ReadAllLinesAsync("day3.txt");
        var slopes = new[]
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2)
        };

        var trees = slopes
            .Select(s => map.TraverseForest(s.Item1, s.Item2).Sum())
            .Select(s => (long)s)
            .Aggregate((a, count) => a * count);

        Console.WriteLine($"Product: {trees}");
    }

    internal static IEnumerable<int> TraverseForest(this string[] map, int deltaX, int deltaY)
    {
        var lineLen = map[0].Length;
        var lines = map.Length;
        var x = 0;
        var y = 0;

        while (y < lines - 1)
        {
            y += deltaY;
            x = (x + deltaX) % lineLen;

            var tree = map[y][x] == '#';
            yield return tree ? 1 : 0;
        }
    }
}