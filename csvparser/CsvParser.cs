using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSV.Parser
{
    public class CsvParser<T> where T: new ()
    {
        private ICSVParser _iCSVParser;
        private ICSVItemParser<T> _iCSVItemParser;
        private CsvParser _parser;

        public CsvParser(ICSVParser parser, ICSVItemParser<T> itemParser)
        {
            this._iCSVParser = parser;
            this._iCSVItemParser = itemParser;
            this._parser = new CsvParser(_iCSVParser);
        }

        public CsvParser(ICSVItemParser<T> itemParser)
        {
            this._iCSVItemParser = itemParser;
        }

        public IEnumerable<T> Parse(string path)
        {
            var parsedCsv = _parser.Parse(path);
            
            return parsedCsv.Select(i => _iCSVItemParser.Map(i, _parser.Headers));
        }
    }

    public class CsvParser
    {
        private ICSVParser _parser;

        public string[] Headers { get; private set; }

        public CsvParser(ICSVParser parser)
        {
            this._parser = parser;
        }
        public IEnumerable<string[]> Parse(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string line;

                if (_parser.HasHeader)
                    this.Headers = _parser.ParseLine(reader.ReadLine());

                while ((line = reader.ReadLine()) != null)
                {
                    var result = _parser.ParseLine(line);

                    if (result.Length != Headers.Length)
                        throw new CsvParserException();

                    yield return result;
                }
            }
        }
    }
}
