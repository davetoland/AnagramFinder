namespace AnagramFinder
{
    /// <summary>
    /// Reads a word stream and creates chunks of same size words
    /// Chunks 
    /// </summary>
    public class WordAnalyser
    {
        private static readonly AnagramParser _parser = new();

        // Enumerator pattern, so that we can read and parse line by line
        // Reading the whole file into memory could cause issues
        public static IEnumerable<string> FindAnagrams(string filepath)
        {
            // Dispose of this when done
            using var fs = new FileStreamer();
            if (!fs.OpenFile(filepath, out var pathError))
            {
                // If the path is bad, just return the error and exit
                yield return pathError;
                yield break;
            }

            int? current = null;
            var cache = new List<string>();

            while (true)
            {
                var line = fs.ReadLine();
                // If the file is empty just exit
                if (line.Length == 0)
                    yield break;

                // If this is the first loop, current will be null
                // so just set it to the length of the first line
                current ??= line.Length;

                // Change in length means new set, pause and process this one
                if (line.Length != current)
                {
                    // Repoint the list then clear it
                    var words = cache.ToList();
                    cache.Clear();

                    List<string> anagrams = new();
                    string? parserError = null;

                    try
                    {
                        // We're in an enumerator here, so catch and handle
                        // any exceptions
                        anagrams = _parser.FindAnagrams(words);
                    }
                    catch (FormatException fe)
                    {
                        // Can't yield from within here, so note the error and move on
                        parserError = fe.Message;
                    }

                    // If we did get an exception, gracefull exit out of the enumerator
                    if (parserError != null)
                    {
                        yield return parserError;
                        yield break;
                    }

                    // Wait for user to enumerate whole list (or as many as they choose)
                    if (anagrams.Any())
                        foreach (var anagram in anagrams)
                            yield return anagram;

                    // Reset for the new set
                    current = line.Length;
                }

                // Add the new line after yielding any potential previous results
                cache.Add(line);
            }
        }
    }
}
