namespace AnagramFinder
{
    public class ReaderCache : IDisposable
    {
        private readonly List<string> _cache;
        private readonly StreamReader _streamReader;

        public ReaderCache(string filepath) 
        {
            _cache = new List<string>();
            _streamReader = new StreamReader(File.OpenRead(filepath));
        }

        public int ReadLine()
        {
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
            _streamReader.Close();
        }
    }
}
