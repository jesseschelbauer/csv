namespace CSV.Parser
{
    public class DefaultCSVParser : ICSVParser
    {
        public bool HasHeader => true;
        public string ItemSeparator => ",";
        public string[] ParseLine(string line)
        {
            return line.Split(ItemSeparator);
        }
    }
}
