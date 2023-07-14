using FluentAssertions;

namespace AnagramFinder.Tests
{
    public class WordAnalyserTests
    {
        [Test]
        public void FindAnagrams_InvalidPath_ReturnsError()
        {
            var result = WordAnalyser.FindAnagrams(FileStreamerTests.InvalidPath);
            result.Single().Should().Be($"Invalid file path: {FileStreamerTests.InvalidPath}");
        }

        [Test]
        public void FindAnagrams_EmptyFile_ReturnsEmpty()
        {
            var result = WordAnalyser.FindAnagrams(FileStreamerTests.EmptyFile);
            result.Should().BeEmpty();
        }
    }
}