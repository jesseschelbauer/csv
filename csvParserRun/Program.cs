using System;
using System.IO;
using System.Linq;
using ConsoleTable;
using CSV.Parser;

namespace CsvParserRun
{
    public class Car 
    {
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fileExists = false;
            var validExtension = false;
            var path = string.Empty;

            while (!fileExists || !validExtension)
            {
                PrintReadMessage();
                path = Console.ReadLine();

                if (path == "exit")
                {
                    Console.WriteLine("Exit....");
                    return;
                }

                fileExists = File.Exists(path);
                validExtension = Path.GetExtension(path).ToLower() == ".csv";
                LogMessages(fileExists, validExtension);
            }

            Console.WriteLine("Processing file");

            var csvParser = new CsvParser(new DefaultCSVParser());
            var parsetData = csvParser.Parse(path);

            var consoleTable = new PrintTable(parsetData, csvParser.Headers);

            consoleTable.Print();

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

        private static void PrintReadMessage()
        {
            Console.WriteLine("This implementations suports the default no quoted csv -> a,b,12,v,ddddd ");
            Console.WriteLine("Type the file path or exit:");
        }
    }
}
