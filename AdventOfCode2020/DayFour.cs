using System.Text.RegularExpressions;

internal static class DayFour
{
    internal static async Task RunFirst()
    {
        var data = await File.ReadAllLinesAsync("day4.txt");
        var mandatoryTags = new[]
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };
        var passports = data
            .ParsePassports()
            .Count(x => mandatoryTags.All(y => x.ContainsKey(y)));

        Console.WriteLine($"{passports} valid passports");
    }

    internal static async Task RunSecond()
    {
        var data = await File.ReadAllLinesAsync("day4.txt");
        var rules = new Dictionary<string, Func<string, bool>>
        {
            ["byr"] = v => v.Length == 4 && int.Parse(v) >= 1920 && int.Parse(v) <= 2002,
            ["iyr"] = v => v.Length == 4 && int.Parse(v) >= 2010 && int.Parse(v) <= 2020,
            ["eyr"] = v => v.Length == 4 && int.Parse(v) >= 2020 && int.Parse(v) <= 2030,
            ["hgt"] = v =>
            {
                var m = Regex.Match(v, @"(\d*)(\w*)");
                if (!int.TryParse(m.Groups[1].Value, out var hgt))
                {
                    return false;
                }

                return m.Groups[2].Value switch
                {
                    "in" => hgt >= 59 && hgt <= 76,
                    "cm" => hgt >= 150 && hgt <= 193,
                    _ => false
                };
            },
            ["hcl"] = v => Regex.Match(v, @"\#[0-9a-f]{6}").Success,
            ["ecl"] = v => new[] {"amb","blu","brn","gry","grn","hzl","oth"}.Contains(v),
            ["pid"] = v => v.Length == 9 && long.TryParse(v, out _)
        };

        var passports = data
            .ParsePassports()
            .Where(p => rules.All(v => p.TryGetValue(v.Key, out var value) && v.Value(value)))
            .Count();

        Console.WriteLine($"{passports} valid passports");
    }

    internal static IEnumerable<Dictionary<string, string>> ParsePassports(this IEnumerable<string> lines)
    {
        var regex = new Regex(@"(\w{3}):(#?\w*)", RegexOptions.Compiled | RegexOptions.Multiline);
        using var linesEnum = lines.GetEnumerator();
        List<string> buffer = new();

        while (linesEnum.MoveNext())
        {
            var line = linesEnum.Current;
            if (line.Length > 0)
            {
                buffer.Add(line);
                continue;
            }

            var tags = ParseBuffer();
            yield return tags;
        }

        if (buffer.Any())
            yield return ParseBuffer();

        yield break;

        Dictionary<string, string> ParseBuffer()
        {
            var tags = buffer
                .Select(l => regex.Matches(l))
                .SelectMany(x => x)
                .ToDictionary(
                    m => m.Groups[1].Value,
                    m => m.Groups[2].Value
                )
                ;

            buffer.Clear();
            return tags;
        }
    }
}