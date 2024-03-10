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

    static void PrintUsage()
    {
        Console.WriteLine("Usage: ZimToMarkdown.exe <zim file> <markdown file>");
    }

    static int Main(string[] args)
    {
        if (args.Length != 2)
        {
            PrintUsage();
            return (int)ExitCode.InvalidArguments;
        }

        var inputFileName = args[0];
        var outputFileName = args[1];
        string[]? inputLines;

        try
        {
            inputLines = File.ReadAllLines(inputFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when reading from {inputFileName}: {ex.Message}");
            return (int)ExitCode.ErrorReadingInputFile;
        }

        IConverter converter = new Converter();
        var outputLines = ConvertLines(inputLines, converter);

        try
        {
            File.WriteAllLines(outputFileName, outputLines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when writing to {outputFileName}: {ex.Message}");
            return (int)ExitCode.ErrorWritingOutputFile;
        }

        return (int)ExitCode.Success;
    }
}
