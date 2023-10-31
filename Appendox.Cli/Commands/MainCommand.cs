using Appendox.Cli.Validators;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using Spectre.Console;

namespace Appendox.Cli.Commands;

[Command]
public sealed class MainCommand : ICommand
{
    [CommandParameter(
        0,
        Description = "The path of the source code to be converted. Can be either an URL or local directory path. Uses current directory by default.",
        Name = "sourcePath",
        Validators = new[] { typeof(PathValidator) },
        IsRequired = false
    )]
    public string? SourcePath { get; private set; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        SourcePath ??= Directory.GetCurrentDirectory();
        AnsiConsole.MarkupLineInterpolated($"The specified source path is: [green]{SourcePath}[/]");

        await AnsiConsole
            .Status()
            .Spinner(Spinner.Known.Dots8Bit)
            .SpinnerStyle(Style.Parse("yellow bold"))
            .StartAsync(
                "Processing repository...",
                async ctx =>
                {
                    await Task.Delay(1000);
                    AnsiConsole.WriteLine("Ha");

                    await Task.Delay(1000);
                    AnsiConsole.WriteLine("Ha");

                    await Task.Delay(1000);
                    AnsiConsole.WriteLine("Ha");
                }
            );
    }
}
