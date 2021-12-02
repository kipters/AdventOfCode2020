namespace AdventOfCode2020
{
    internal class DayOne
    {
        internal static async Task RunFirst()
        {
            var numbers = (await File.ReadAllLinesAsync("day1.txt")).Select(int.Parse);
            var pair =
                (from x in numbers
                 from y in numbers
                 select (x, y))
                .First(t => t.x + t.y == 2020)
                ;

            Console.WriteLine($"Pair: {pair}, product: {pair.x * pair.y}");
        }

        internal static async Task RunSecond()
        {
            var numbers = (await File.ReadAllLinesAsync("day1.txt")).Select(int.Parse);
            var triplet = 
                (from x in numbers
                 from y in numbers
                 from z in numbers
                 select (x, y, z))
                .First(t => t.x + t.y + t.z == 2020)
                ;

            Console.WriteLine($"Triplet: {triplet}, product: {triplet.x * triplet.y * triplet.z}");
        }
    }
}
