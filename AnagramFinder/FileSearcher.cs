namespace AnagramFinder
{
    public class FileSearcher
    {
        public static IEnumerable<string> FindAnagrams(string filepath)
        {
            using var reader = new ReaderCache(filepath);
            int? currentLength = null;

            while (true)
            {
                var length = reader.ReadLine();
                if (length == 0)
                    yield break;

                currentLength ??= length;

                if (length != currentLength)
                {
                    var words = reader.GetLines();
                    var anagrams = WordParser.FindAnagrams(words);

                    if (anagrams.Any())
                        foreach (var anagram in anagrams)
                            yield return anagram;

                    currentLength = length;
                }
            }
        }
    }
}
