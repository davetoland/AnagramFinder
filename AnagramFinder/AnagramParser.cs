namespace AnagramFinder
{
    /// <summary>
    /// Builds collections of anagrams from a word list
    /// </summary>
    public class AnagramParser
    {
        // Key = alpha sorted variant, Value = unique list of variants
        private Dictionary<string, List<string>> _anagrams;

        public AnagramParser()
        {
            _anagrams = new Dictionary<string, List<string>>();
        }

        public List<string> FindAnagrams(List<string> words)
        {
            _anagrams.Clear();

            // Ignore this input
            if (words == null || words.Count == 0)
                return Enumerable.Empty<string>().ToList();

            // This is against protocol and unprocessable, so exceptional
            if (!words.All(x => x.Length == words.First().Length))
                throw new FormatException(FormatError);

            foreach (var word in words)
            {
                // Split chars, reorder alphabetically and reassemble
                var alpha = string.Concat(word.OrderBy(c => c));

                // Single key for each unique variation
                if (!_anagrams.ContainsKey(alpha))
                    // Spin up a new list as it's the first entry
                    //Note: side effect, we're adding 
                    _anagrams.Add(alpha, new List<string> { word });
                else
                    // Only add this variant if it's unique
                    if (!_anagrams[alpha].Contains(word))
                        _anagrams[alpha].Add(word);
            }

            // As all unique alpha variants are added 
            return _anagrams.Values
                .Where(x => x.Count > 1)
                .Select(x => string.Join(",", x))
                .ToList();
        }

        internal const string FormatError = "All words must be the same length";
    }
}
