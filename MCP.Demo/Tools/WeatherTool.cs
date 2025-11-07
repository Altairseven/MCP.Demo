using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Diagnostics;

namespace MCP.Demo.Tools;

[McpServerToolType]
public static class WeatherTool
{
    [McpServerTool(Name = "clima"), Description("devuelve una lista de pronosticos de clima para hoy")]
    public static string Echo() 
    {
        var rnd = new Random().Next(1, 3);

        var result = rnd == 1 ? "Soleado" : rnd == 2 ? "Nublado" : "Lluvioso";

        return result;
    }
}
