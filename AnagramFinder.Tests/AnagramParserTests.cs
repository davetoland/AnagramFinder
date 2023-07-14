using FluentAssertions;

namespace AnagramFinder.Tests
{
    public class AnagramParserTests
    {
        private AnagramParser _parser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _parser = new AnagramParser();
        }

        [Test]
        public void FindAnagrams_NullOrEmptyInput_ReturnsEmptyList()
        {
            var result = _parser.FindAnagrams(null);
            result.Should().HaveCount(0);
        }

        [Test]
        public void FindAnagrams_DifferingWordLengths_ThrowsFormatException() =>
            _parser.Invoking(x => x.FindAnagrams(new List<string> { "aaa", "bbbb" }))
                .Should().Throw<FormatException>()
                .WithMessage(AnagramParser.FormatError);

        [Test]
        public void FindAnagrams_ThreeWordVariation_ReturnsOneAnagram()
        {
            var words = new List<string> { "abc", "bca", "cab" };
            var result = _parser.FindAnagrams(words);
            result.Should().HaveCount(1);
            result.Single().Should().Be("abc,bca,cab");
        }

        [Test]
        public void FindAnagrams_MultipleVariations_ReturnsMultipleAnagrams()
        {
            var words = new List<string> { "abc", "bca", "cab", "mno", "nom", "omn", "xyz", "yxz", "zyx" };
            var result = _parser.FindAnagrams(words);
            result.Should().HaveCount(3);
            result.Should().ContainInOrder("abc,bca,cab", "mno,nom,omn", "xyz,yxz,zyx");
        }
    }
}