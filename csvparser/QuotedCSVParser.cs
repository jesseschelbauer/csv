using System.Linq;
using System.Text.RegularExpressions;

namespace CSV.Parser
{
    public class QuotedCSVParser : ICSVParser
    {
        public bool HasHeader => true;
        public string ItemSeparator => ",";
        public string[] ParseLine(string line)
        {
            MatchCollection matches = new Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))").Matches(line);
            return matches.Select(m => m.Value).ToArray();
        }
    }
}
