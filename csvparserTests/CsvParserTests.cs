using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CSV.Parser.Tests
{
    [TestClass()]
    public class CsvParserTests
    {
        [TestMethod()]
        [DeploymentItem("csvModels\\cars.csv")]
        [DeploymentItem("csvModels\\1.csv")]
        [DeploymentItem("csvModels\\2.csv")]
        [DeploymentItem("csvModels\\3.csv")]
        [DeploymentItem("csvModels\\4.csv")]
        [DataRow("1.csv", 20, 9)]
        [DataRow("2.csv", 20, 7)]
        [DataRow("3.csv", 50, 14)]
        [DataRow("4.csv", 6000, 5)]
        public void ParseQuotedParserTest(string file, int rows, int cols )
        {
            var csvParser = new CsvParser(new QuotedCSVParser());
            var cars = csvParser.Parse(file);
            Assert.IsTrue(cars.Count() == rows);
            Assert.IsTrue(cars.Select(a => a.Length).GroupBy(a => a).Count() == 1, "There are itens with diferent sizes.");            
        }

        [TestMethod()]
        [DeploymentItem("csvModels\\5.csv")]
        [ExpectedException(typeof(CsvParserException))]
        public void ParseQuotedParser_Wronglayout_Test()
        {
            var csvParser = new CsvParser(new QuotedCSVParser());
             csvParser.Parse("5.csv").ToList();
        }
    }
}

