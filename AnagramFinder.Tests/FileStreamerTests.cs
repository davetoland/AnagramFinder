using FluentAssertions;

namespace AnagramFinder.Tests
{
    public class FileStreamerTests
    {
        private FileStreamer _fs;

        [SetUp]
        public void Setup()
        {
            _fs = new FileStreamer();
        }

        [Test]
        public void OpenFile_InvalidPath_ReturnsFalseAndError()
        {
            var success = _fs.OpenFile(InvalidPath, out var error);
            success.Should().BeFalse();
            error.Should().Be($"Invalid file path: {InvalidPath}");
        }

        [Test]
        public void OpenFile_ValidPath_ReturnsTrueWithNoError()
        {
            var success = _fs.OpenFile(ThreeLines, out var error);
            success.Should().BeTrue();
            error.Should().BeEmpty();
        }

        [Test]
        public void ReadLine_Uninitialised_ReturnsEmpty()
        {
            var result = _fs.ReadLine();
            result.Length.Should().Be(0);
        }

        [Test]
        public void ReadLine_NullRead_ReturnsEmpty()
        {
            _fs.OpenFile(EmptyFile, out var _);
            var result = _fs.ReadLine();
            result.Length.Should().Be(0);
        }

        [Test]
        public void ReadLine_ValidLine_ReturnsPositive()
        {
            _fs.OpenFile(ThreeLines, out var _);
            var result = _fs.ReadLine();
            result.Length.Should().BeGreaterThan(0);
        }

        public const string ThreeLines = "TestData/three.txt";
        public const string EmptyFile = "TestData/empty.txt";
        public const string InvalidPath = "this//is..garbage";
    }
}