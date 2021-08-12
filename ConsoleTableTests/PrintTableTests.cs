using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleTable.Tests
{
    /// <summary>
    /// TODO This test is not ready. finda a way to compare de genareted table
    /// </summary>
    [TestClass()]
    public class PrintTableTests
    {
        [TestMethod()]
        [DeploymentItem("csvModels\\print.csv")]
        public void PrintTest()
        {
            var result = 
@"┌────────┬───────┬────┬─────────┬────────┐
│anzsic06│Area   │year│geo_count│ec_count│
├────────┼───────┼────┼─────────┼────────┤
│A       │A100100│2000│96       │130     │
├────────┼───────┼────┼─────────┼────────┤
│A       │A100200│2000│198      │110     │
├────────┼───────┼────┼─────────┼────────┤
│A013    │A106700│2000│9        │0       │
├────────┼───────┼────┼─────────┼────────┤
│A013    │A106900│2000│6        │0       │
└────────┴───────┴────┴─────────┴────────┘";

            var headers = new string[] { "anzsic06", "Area", "year", "geo_count", "ec_count" };

            var data = new List<string[]> {
                
                new string[] {"A", "A100100","2000","96","130"},
                new string[] {"A","A100200","2000","198","110"},
                new string[] {"A013","A106700","2000","9","0"},
                new string[] {"A013","A106900","2000","6","0"},
            };

            PrintTable printTable = new PrintTable(data, headers);
            
            using (var sw = new StringWriter()) 
            {
                Console.SetOut(sw);
                printTable.Print();
                Assert.AreEqual(result, sw.ToString());
            }

        }
    }
}
