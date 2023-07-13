namespace AnagramFinder
{
    public class WordParser
    {
        public static List<string> FindAnagrams(List<string> words)
        {
            if (words.All(x => x.Length == words.First().Length))
                throw new FormatException("All words must be the same length");

            var anagrams = new Dictionary<string, List<string>>();

            foreach (var word in words)
            {
                var alpha = string.Concat(word.OrderBy(c => c));

                if (!anagrams.ContainsKey(alpha))
                    anagrams.Add(alpha, new List<string> { word });
                else
                    if (!anagrams[alpha].Contains(word))
                        anagrams[alpha].Add(word);
            }

            return anagrams.Values
                .Where(x => x.Count > 1)
                .Select(x => string.Join(",", x))
                .ToList();
        }
    }
}
