namespace ZimToMarkdown;

internal class Program
{
    enum ExitCode : int
    {
        Success = 0,
        InvalidArguments = 1,
        ErrorReadingInputFile = 2,
        ErrorWritingOutputFile = 3,
    }

    static IEnumerable<string> ConvertLines(IEnumerable<string> inputLines, IConverter converter)
    {
        var outputLines = new List<string>();
        int lineNumber = 0;
        foreach (var line in inputLines)
        {
            // skip the first four lines
            if (lineNumber++ < 4)
            {
                continue;
            }

            outputLines.Add(converter.Convert(line));
        }

        return outputLines;
    }

    // TODO: Move this and ConvertLines into a separate class
    static ExitCode Convert(string inputFileName, string outputFileName)
    {
        string[]? inputLines;

        try
        {
            inputLines = File.ReadAllLines(inputFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when reading from {inputFileName}: {ex.Message}");
            return ExitCode.ErrorReadingInputFile;
        }

        IConverter converter = new ZimToMarkdownConverter();
        var outputLines = ConvertLines(inputLines, converter);

        try
        {
            File.WriteAllLines(outputFileName, outputLines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when writing to {outputFileName}: {ex.Message}");
            return ExitCode.ErrorWritingOutputFile;
        }

        return ExitCode.Success;
    }

    static int Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ZimToMarkdown.exe <zim file path> <markdown file path>");
            return (int)ExitCode.InvalidArguments;
        }

        return (int)Convert(args[0], args[1]);
    }
}
