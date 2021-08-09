namespace CSV.Parser
{
    public interface ICSVItemParser<T> where T : new()
    {
        T Map(string[] line, string[] headers);
    }
}
