using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCP.Demo.Tools;

[McpServerToolType]
public static class MessageTools
{

    [McpServerTool(Name = "count_characters"), Description("it returns the number of characters that a given message contains")]
    public static string CountCharacters(string message)
    {
        var count = message.Length;
        return $"From C# Code: The number of characters is {count}";
    }
}
