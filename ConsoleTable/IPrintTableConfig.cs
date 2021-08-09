namespace ConsoleTable
{
    public interface IPrintTableConfig
    {
        (char, char, char) TopChars { get; }
        (char, char, char) MiddleChars { get; }
        (char, char, char) BottomChars { get; }
        char ColumnChar { get; }
        char RowChar { get; }
    }
}