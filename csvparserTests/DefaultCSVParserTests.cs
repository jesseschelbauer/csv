﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSV.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV.Parser.Tests
{
    [TestClass()]
    public class DefaultCSVParserTests
    {
        [TestMethod()]
        public void ParseLineTestQuoted()
        {
            var quotedCSVParser = new QuotedCSVParser();
            var values = quotedCSVParser.ParseLine(@"1999,Chevy,""Venture Extended Edition, Very Large"",,5000.00");
            Assert.AreEqual(5, values.Length);
        }

        [TestMethod()]
        public void ParseLineTest()
        {
            var quotedCSVParser = new QuotedCSVParser();
            var values = quotedCSVParser.ParseLine(@"1999,Chevy,Venture Extended Edition Very Large,,5000.00");
            Assert.AreEqual(5, values.Length);
        }
    }
}