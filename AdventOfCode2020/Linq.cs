namespace AdventOfCode2020
{
    internal static class Linq
    {
        public static IEnumerable<T[]> SlidingWindow<T>(this IEnumerable<T> sequence, int size)
        {
            var window = new List<T>(size);

            using var src = sequence.GetEnumerator();

            for (int i = 0; i < size - 1; i++)
            {
                if (src.MoveNext())
                    window.Add(src.Current);
            }

            while (src.MoveNext())
            {
                window.Add(src.Current);
                if (window.Count > size)
                {
                    window.RemoveAt(0);
                }

                yield return window.ToArray();
            }
        }

        public static IEnumerable<IEnumerable<string>> LineChunks(this IEnumerable<string> lines)
        {
            using var src = lines.GetEnumerator();
            List<string> chunks = new();

            while (src.MoveNext())
            {
                var line = src.Current;
                if (line.Length > 0)
                {
                    chunks.Add(line);
                    continue;
                }

                yield return chunks;
                chunks.Clear();
            }

            if (chunks.Any())
                yield return chunks;

            yield break;
        }
    }
}
