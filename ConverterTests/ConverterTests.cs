using ZimToMarkdown;
namespace ConverterTests;

[TestClass]
public class ConverterTests
{
    [TestMethod]
    public void ImageLinksAreConvertedCorrectly()
    {
        var input = @"{{.\docs\image.png}}";
        var expected = @"<img src="".\docs\image.png""/>";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CsharpCodeBlocksAreConvertedCorrectly()
    {
        var input = @"{{{code: lang=""c-sharp""";
        var expected = "```cs";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void OtherCodeBlocksAreConvertedCorrectly()
    {
        var input = @"{{{code: lang=""python""";
        var expected = "```python";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TopLevelHeadersAreConvertedCorrectly()
    {
        var input = @"====== Header ======";
        var expected = "# Header";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SecondLevelHeadersAreConvertedCorrectly()
    {
        var input = @"==== Header ====";
        var expected = "## Header";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InlineCodeIsConvertedCorrectly()
    {
        var input = "''code''";
        var expected = "`code`";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LinksAreConvertedCorrectly()
    {
        var input = "[[link]]";
        var expected = "[link](link)";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TabsAreConvertedToSpaces()
    {
        var input = "\t";
        var expected = "    ";
        var converter = new ZimToMarkdownConverter();
        var actual = converter.Convert(input);
        Assert.AreEqual(expected, actual);
    }
}