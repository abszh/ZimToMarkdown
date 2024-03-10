# Zim to Markdown

This C# script converts Zim desktop wiki files to markdown format. It can convert headers, code blocks, inline code, picture links and file links.

# Usage

```
ZimToMarkdown.exe <Zim filename> <Markdown filename>
```

# Examples

## Code blocks

    {{{code: lang="cpp"
    // some code
    }}}

The above Zim code block is converted to the below Markdown code block.


    ```cpp
    // some code
    ```

## Headers

    ====== Header 1 ======

The above Zim header is converted to the below Markdown header.

    # Header 1

## Links

    [[.\filename.txt]]

The above Zim link is converted to the below Markdown link.

    [.\filename.txt](filename.txt)

## Images

    {{.\docs\image.png}}

The above image link is converted to the below Markdown image link.

    <img src=".\docs\image.png"/>
