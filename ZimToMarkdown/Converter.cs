using System.Text.RegularExpressions;

namespace ZimToMarkdown;

internal class Converter : IConverter
{
    public string Convert(string input)
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
        line = Regex.Replace(line, @"======([^=]*)\s======", "#$1");
        line = Regex.Replace(line, @"=====([^=]*)\s=====", "#$1");
        line = Regex.Replace(line, @"====([^=]*)\s====", "##$1");

        // inline code
        line = Regex.Replace(line, "''([^']+)''", "`$1`");

        // pictures
        line = Regex.Replace(line, @"\{\{\.\\([^\}]*)\}\}", @"<img src="".\$1""/>");

        // replace tabs with four spaces
        line = Regex.Replace(line, "\t", "    ");

        // link to file
        line = Regex.Replace(line, @"\[\[([^\]]*)\]\]", "[$1]($1)");

        return line;
    }
}
