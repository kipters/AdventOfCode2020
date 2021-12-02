using AdventOfCode2020;

internal class DayFive
{
    internal static async Task RunFirst()
    {
        var lines = await File.ReadAllLinesAsync("day5.txt");

        var maxSeat = lines
            .Select(ParseSeatLine)
            .Select(c => (c.row * 8) + c.seat)
            .Max()
            ;

        Console.WriteLine($"Max seat ID: {maxSeat}");
            
    }

    private static (int row, int seat) ParseSeatLine(string l)
    {
        var row = l[..7]
            .Aggregate(
                (min: 0, max: 127),
                (co, c) => c switch
                {
                    'F' => (co.min, (co.max + co.min) / 2),
                    'B' => ((co.max + co.min + 2) / 2, co.max),
                    _ => throw new Exception($"Only F or B expected, got {c}")
                },
                co => co.min
            );

        var seat = l[^3..]
            .Aggregate(
                (min: 0, max: 7),
                (co, c) => c switch
                {
                    'L' => (co.min, (co.max + co.min) / 2),
                    'R' => ((co.max + co.min + 2) / 2, co.max),
                    _ => throw new Exception($"Only L or R expected, got {c}")
                },
                co => co.min
            );

        return (row, seat);
    }

    internal static async Task RunSecond()
    {
        var lines = await File.ReadAllLinesAsync("day5.txt");

        var mySeat = lines
            .Select(ParseSeatLine)
            .Select(c => (c.row * 8) + c.seat)
            .OrderBy(x => x)
            .SlidingWindow(2)
            .First(w => w[1] - w[0] == 2)
            //.ToArray()
            ;

        Console.WriteLine($"My seat is {mySeat[0] + 1}");
    }
}