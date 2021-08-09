namespace CSV.Parser
{
    public interface ICSVParser
    {
        string[] ParseLine(string line);
        public bool HasHeader { get; }
        public string ItemSeparator { get; }
    }
}
