using FluentAssertions;

namespace AnagramFinder.Tests
{
    public class ReaderCacheTests
    {
        private ReaderCache _readerCache;

        [SetUp]
        public void Setup()
        {
            _readerCache = new ReaderCache();
        }

        [Test]
        public void ReaderCache_InvalidPath_ReturnsFalseAndError()
        {
            var garbage = "this//is..garbage";
            var success = _readerCache.OpenFile(garbage, out var error);
            success.Should().BeFalse();
            error.Should().Be($"Invalid file path: {garbage}");
        }

        [Test]
        public void ReaderCache_ValidPath_ReturnsTrueWithNoError()
        {
            var valid = "TestData/three.txt";
            var success = _readerCache.OpenFile(valid, out var error);
            success.Should().BeTrue();
            error.Should().BeEmpty();
        }
    }
}