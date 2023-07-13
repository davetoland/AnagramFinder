using System.Diagnostics;

namespace AnagramFinder
{
    internal class Program
    {
        static void Main(string[] _)
        {
            var timer = new Stopwatch();
            timer.Start();

            foreach (var anagram in FileSearcher.FindAnagrams("example2.txt"))
                Console.WriteLine(anagram);

            timer.Stop();
            Console.WriteLine("");
            Console.WriteLine($"Finished in {FormatElapsed(timer.Elapsed)}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static string FormatElapsed(TimeSpan elapsed) =>
            elapsed.TotalMilliseconds < 1000
                ? $"{Math.Round(elapsed.TotalMilliseconds, 2)} milliseconds"
                : $"{Math.Round(elapsed.TotalMilliseconds / 1000, 2)} seconds";
    }
}
