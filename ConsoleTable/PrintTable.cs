using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTable
{
    public class PrintTableDefaultConfig : IPrintTableConfig
    {
        public (char, char, char) TopChars => ('┌', '┬', '┐');

        public (char, char, char) MiddleChars => ('├', '┼', '┤');

        public (char, char, char) BottomChars => ('└', '┴', '┘');

        public char ColumnChar => '│';

        public char RowChar => '─';
    }
    public class PrintTable
    {
        private IPrintTableConfig _iPrintTableConfig;

        private string OpenLineTemplate { get; set; }
        private string IntermediateLineTemplate { get; set; }
        private string CloseLineTemplate { get; set; }
        private string DataRowTemplate { get; }
        private IEnumerable<string[]> Data { get; }
        private int[] ColsWidth { get; }
        private int TotalRows { get; }

        public PrintTable(IEnumerable<string[]> data, string[] headers, IPrintTableConfig iPrintTableConfig = null)
        {
            _iPrintTableConfig = iPrintTableConfig ?? new PrintTableDefaultConfig();

            data.Prepend(headers);

            Data = data;

            var firstRow = data.First();
            ColsWidth = new int[firstRow.Length];

            TotalRows = data.Count();
            SetColsWidth(data, firstRow);

            OpenLineTemplate = CreateOpenLineTemplate();
            IntermediateLineTemplate = CreateIntermediateLineTemplete();
            CloseLineTemplate = CreateCloseLineTemplete();
            DataRowTemplate = CreatDataRowTemplate();
        }

        private void SetColsWidth(IEnumerable<string[]> data, string[] firstRow)
        {
            for (int i = 0; i < firstRow.Length; i++)
            {
                ColsWidth[i] = data.Select(r => r[i].Length).Max();
            }
        }

        public void Print()
        {
            Console.WriteLine(OpenLineTemplate);

            foreach (var item in Data.Select((Value, Index) => new { Value, Index }))
            {
                Console.WriteLine(FillRowTemplate(item.Value));
                Console.WriteLine(CreateLine(item.Index));
            }
        }

        private string CreatDataRowTemplate()
        {
            var columnChar = _iPrintTableConfig.ColumnChar;
            return string.Format("{0}{1}{0}", columnChar, string.Join(columnChar, ColsWidth.Select((r, index) => string.Format("{{{0},-{1}}}", index, r))));
        }

        private string FillRowTemplate(string[] rowData)
        {
            return string.Format(DataRowTemplate, rowData);
        }

        private string CreateLine(int index)
        {
            if (index < TotalRows - 1)
                return IntermediateLineTemplate;

            return CloseLineTemplate;
        }

        private string CreateOpenLineTemplate()
        {
            return DrawLine(ColsWidth, _iPrintTableConfig.TopChars);
        }

        private string CreateIntermediateLineTemplete()
        {
            return DrawLine(ColsWidth, _iPrintTableConfig.MiddleChars);
        }

        private string CreateCloseLineTemplete()
        {
            return DrawLine(ColsWidth, _iPrintTableConfig.BottomChars);
        }

        private string DrawLine(int[] colsWidth, (char, char, char) topChars)
        {
            var (left, center, right) = topChars;
            var line = string.Join(center, colsWidth.Select(w => string.Join(string.Empty, Enumerable.Repeat(_iPrintTableConfig.RowChar, w))));
            return string.Format($"{left}{line}{right}");
        }
    }
}

