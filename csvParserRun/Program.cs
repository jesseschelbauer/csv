using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleTable;
using CSV.Parser;

namespace CsvParserRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvProcessOptions = new Dictionary<string, ICSVParser>() {
                { "1",  new DefaultCSVParser()},
                { "2",  new QuotedCSVParser()},
            };

            var fileExists = false;
            var validExtension = false;
            var path = string.Empty;
            var selectedParser = string.Empty;

            while (!fileExists || !validExtension)
            {
                PrintCSVParserOptionMessage();

                selectedParser = Console.ReadLine();

                if (!csvProcessOptions.ContainsKey(selectedParser))
                    continue;

                PrintReadMessage();

                path = Console.ReadLine();

                if (path == "exit" || selectedParser == "exit")
                {
                    Console.WriteLine("Exit....");
                    return;
                }

                fileExists = File.Exists(path);
                validExtension = Path.GetExtension(path).ToLower() == ".csv";
                LogMessages(fileExists, validExtension);
            }

            Console.WriteLine("Processing file");
            try
            {
                var csvParser = new CsvParser(csvProcessOptions[selectedParser]);
                var parsetData = csvParser.Parse(path);

                var consoleTable = new PrintTable(parsetData, csvParser.Headers);

                consoleTable.Print();
            }
            catch (CsvParserException e)
            {
                LogErrorMessage(e.Message);
            }
            catch (Exception) 
            {
                LogErrorMessage("Error!");
            }

            Main(args);
        }

        private static void LogMessages(bool fileExists, bool validExtension)
        {
            if (!fileExists)
            {
                LogErrorMessage("File not found!");
                return;
            }

            if (!validExtension)
            {
                LogErrorMessage("Invaid extension!");
                return;
            }
        }
        private static void LogErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void PrintCSVParserOptionMessage() 
        {
            Console.WriteLine("Type 1 for default csv not quoted comma separated \nType 2 for quoted csv comma separated");
        }

        private static void PrintReadMessage()
        {
            Console.WriteLine("Type the file path or exit:");
        }
    }
}
