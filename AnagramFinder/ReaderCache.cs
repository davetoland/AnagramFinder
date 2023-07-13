namespace AnagramFinder
{
    public class ReaderCache : IDisposable
    {
        internal List<string> _cache;
        internal StreamReader? _streamReader;

        public ReaderCache()
        {
            _cache = new List<string>();
            _streamReader = null;
        }

        public bool OpenFile(string filepath, out string error) 
        {
            if (filepath.Length == 0 || !File.Exists(filepath))
            {
                error = $"Invalid file path: {filepath}";
                return false;
            }

            error = string.Empty;
            _streamReader = new StreamReader(File.OpenRead(filepath));
            return true;
        }

        public int ReadLine()
        {
            if (_streamReader == null)
            {
                Console.Error.WriteLine("Uninitialised stream reader, call OpenFile() first");
                return -1;
            }

            var line = _streamReader.ReadLine();

            if (line == null)
                return 0;

            _cache.Add(line);
            return line.Length;
        }

        public List<string> GetLines()
        {
            var result = _cache.ToList();
            _cache.Clear();
            return result;
        }

        public void Dispose()
        {
            _cache.Clear();
            _streamReader?.Close();
        }
    }
}
