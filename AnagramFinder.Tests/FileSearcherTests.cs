using FluentAssertions;

namespace AnagramFinder.Tests
{
    public class FileSearcherTests
    {
        [Test]
        public void FindAnagrams_InvalidPath_ReturnsError()
        {
            var garbage = "this//is//garbage";
            var result = FileSearcher.FindAnagrams(garbage);
            result.Single().Should().Be($"Invalid file path: {garbage}");
        }

        [Test]
        public void FindAnagrams_EmptyFile_ReturnsEmpty()
        {
            var result = FileSearcher.FindAnagrams("TestData/empty.txt");
            result.Should().BeEmpty();
        }
    }
}