using System.Text.RegularExpressions;

internal class DaySeven
{
    internal static async Task RunFirst()
    {
        var data = await File.ReadAllTextAsync("day7.txt");
        var mainRegex = new Regex(@"^([a-z ]+) bags contain (.*)", RegexOptions.Compiled | RegexOptions.Multiline);
        var secRegex = new Regex(@"(\d) ([a-z ]+) bag[s]?[.|,]", RegexOptions.Compiled);

        var contains = mainRegex.Matches(data)
            .Select(m => (outer: m.Groups[1].Value, rawInner: m.Groups[2].Value))
            .ToDictionary(
                m => m.outer,
                m => secRegex
                    .Matches(m.rawInner)
                    .ToDictionary(
                        i => i.Groups[2].Value,
                        i => int.Parse(i.Groups[1].Value)
                    )
            )
            ;

        var containedBy = contains
            .Select(x => (outer: x.Key, inners: x.Value.Select(r => r.Key)))
            .Select(x => x.inners.Select(y => (contained: y, container: x.outer)))
            .SelectMany(x => x)
            .GroupBy(x => x.contained, x => x.container)
            .ToDictionary(x => x.Key, x => x.ToArray())
            ;

        var allBags = containedBy.Keys.Where(x => x != "shiny gold").ToList();
        var collected = new List<string> { "shiny gold" };

        while (true)
        {
            var count = collected.Count;

            var newBags = collected
                .Where(x => containedBy.ContainsKey(x))
                .SelectMany(x => containedBy[x]);

            collected = collected
                .Concat(newBags)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (count == collected.Count)
                break;
        }

        Console.WriteLine($"{collected.Count - 1} bag colors can contain at least one shiny gold bag");
    }

    internal static async Task RunSecond()
    {
        var data = await File.ReadAllTextAsync("day7.txt");
        var mainRegex = new Regex(@"^([a-z ]+) bags contain (.*)", RegexOptions.Compiled | RegexOptions.Multiline);
        var secRegex = new Regex(@"(\d) ([a-z ]+) bag[s]?[.|,]", RegexOptions.Compiled);

        var contains = mainRegex.Matches(data)
            .Select(m => (outer: m.Groups[1].Value, rawInner: m.Groups[2].Value))
            .ToDictionary(
                m => m.outer,
                m => secRegex
                    .Matches(m.rawInner)
                    .ToDictionary(
                        i => i.Groups[2].Value,
                        i => int.Parse(i.Groups[1].Value)
                    )
            )
            ;

        var children = GetChildren("shiny gold");

        Console.WriteLine($"A shiny gold bag must contain {children - 1} bags");

        int GetChildren(string color) => contains[color].Sum(x => GetChildren(x.Key) * x.Value) + 1;
    }
}