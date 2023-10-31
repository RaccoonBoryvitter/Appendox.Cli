using CliFx;

namespace Appendox.Cli;

public static class Program
{
    public static async Task<int> Main() =>
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .SetTitle("appendox")
            .SetDescription(
                "A small console tool which generates a document file from source code."
            )
            .Build()
            .RunAsync();
}
