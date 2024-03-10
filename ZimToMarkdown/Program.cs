using System.Text.RegularExpressions;

namespace ConvertFromZimToMarkdown;

internal class Program
{
    enum ExitCode : int
    {
        Success = 0,
        InvalidArguments = 1,
        ErrorReadingInputFile = 2,
        ErrorWritingOutputFile = 3,
    }

    static string Convert(string input)
    {
        // c-sharp code block opening tag
        var line = Regex.Replace(input, @"\{\{\{code: lang=""c-sharp"".*", "```cs");

        // other code blocks opening tag
        line = Regex.Replace(line, @"\{\{\{code: lang=""([^""]+)"".*", "```$1");

        // code block closing tag
        line = Regex.Replace(line, "}}}", "```");

        // code block with no language specified
        line = Regex.Replace(line, "'''", "```");

        // headers
        line = Regex.Replace(line, "======([^=]*)\\s======", "#$1");
        line = Regex.Replace(line, "=====([^=]*)\\s=====", "#$1");
        line = Regex.Replace(line, "====([^=]*)\\s====", "##$1");

        // inline code
        line = Regex.Replace(line, "''([^']+)''", "`$1`");

        // pictures
        line = Regex.Replace(line, "\\{\\{\\.\\/([^\\}]*)\\}\\}", "<img src=\"./$1\"/>");

        // replace tabs with four spaces
        line = Regex.Replace(line, "\t", "    ");

        // link to file
        line = Regex.Replace(line, "\\[\\[([^\\]]*)\\]\\]", "[$1]($1)");

        return line;
    }

    static IEnumerable<string> ConvertLines(IEnumerable<string> inputLines)
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

            outputLines.Add(Convert(line));
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

        var outputLines = ConvertLines(inputLines);

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