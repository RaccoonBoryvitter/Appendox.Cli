using Appendox.Cli.Utils;
using Appendox.Cli.Validators;
using Appendox.Core.Exceptions;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using LanguageExt;
using Spectre.Console;
using System.Diagnostics;

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
        var cancellationToken = console.RegisterCancellationHandler();

        await AnsiConsole
            .Status()
            .Spinner(Spinner.Known.Dots8Bit)
            .SpinnerStyle(Style.Parse("blue bold"))
            .StartAsync(
                "Checking Git version...",
                async ctx =>
                {
                    var gitVersionResult = await GitAdapter.GetGitVersionAsync(cancellationToken);
                    _ = gitVersionResult.IfLeft(l => throw new GitCliException(l));
                    var gitVersion = gitVersionResult.IfRight(r => r);

                    AnsiConsole.MarkupLineInterpolated(
                        $"[default on green]SUCCESS[/] Checking Git version: {gitVersion!}"
                    );
                }
            );

        AnsiConsole.MarkupLineInterpolated($"The specified source path is: [green]{SourcePath}[/]");
    }
}
