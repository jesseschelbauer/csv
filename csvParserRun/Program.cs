﻿using System;
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
            
            var csvParser = new CsvParser(new QuotedCSVParser());
            var a = csvParser.Parse(path);

            var result = a.Select(a => a).ToList();

            var consoleTable = new PrintTable(result, csvParser.Headers);

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
            Console.WriteLine("File path or exit:");
        }
    }
}