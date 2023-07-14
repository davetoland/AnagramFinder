namespace AnagramFinder
{
    /// <summary>
    /// Streams a text file line by line.
    /// Maintains an open StreamReader, should be 
    /// instantiated and disposed within a using statement
    /// </summary>
    public class FileStreamer : IDisposable
    {
        private StreamReader? _streamReader = null;

        // Obv we don't want to read the whole file into memory
        // So stream it to a reader which will track our position in the stream
        // By calling this from an enumerator, theoretically it could load an
        // infinitely sized file and still return results (of the same length) in constant time
        public bool OpenFile(string filepath, out string error) 
        {
            // Generally you'd abstract error checking
            // out to some Verify method or process...
            if (filepath.Length == 0 || !File.Exists(filepath))
            {
                error = $"Invalid file path: {filepath}";
                return false;
            }

            // Other checks here

            error = string.Empty;
            _streamReader = new StreamReader(File.OpenRead(filepath));
            return true;
        }

        public string ReadLine()
        {
            if (_streamReader == null)
                return string.Empty;

            var line = _streamReader.ReadLine();
            if (line == null)
                return string.Empty;

            return line;
        }

        public void Dispose()
        {
            _streamReader?.Close();
        }
    }
}
