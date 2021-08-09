using System;
using System.Runtime.Serialization;

namespace CSV.Parser
{
    [Serializable]
    public class CsvParserException : Exception
    {
        public CsvParserException() : base("Wrong layout")
        {
        }
    }
}