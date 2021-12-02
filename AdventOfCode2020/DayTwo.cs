using System.Text.RegularExpressions;

internal class DayTwo
{
    internal static async Task RunFirst()
    {
        var regex = new Regex(@"(\d*)-(\d*)\ (.):\ (\w*)", RegexOptions.Compiled);
        var lines = await File.ReadAllLinesAsync("day2.txt");
        var validPasswords =
            from l in lines
            let groups = regex.Matches(l)[0].Groups
            let min = int.Parse(groups[1].Value)
            let max = int.Parse(groups[2].Value)
            let target = groups[3].Value[0]
            let password = groups[4].Value
            let targetCount = password.Count(c => c == target)
            where targetCount >= min && targetCount <= max
            select password;

        Console.WriteLine($"{validPasswords.Count()} valid passwords");
    }

    internal static async Task RunSecond()
    {
        var regex = new Regex(@"(\d*)-(\d*)\ (.):\ (\w*)", RegexOptions.Compiled);
        var lines = await File.ReadAllLinesAsync("day2.txt");
        var validPasswords =
            from l in lines
            let groups = regex.Matches(l)[0].Groups
            let firstIndex = int.Parse(groups[1].Value) - 1
            let secondIndex = int.Parse(groups[2].Value) - 1
            let target = groups[3].Value[0]
            let password = groups[4].Value
            where password[firstIndex] == target ^ password[secondIndex] == target
            select password;

        Console.WriteLine($"{validPasswords.Count()} valid passwords");
    }
}