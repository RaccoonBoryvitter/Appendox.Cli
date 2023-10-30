using Appendox.Cli.Validators;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Appendox.Cli.Commands;

internal sealed class MainCommand : AsyncCommand<MainCommand.Settings>
{
    internal sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[sourcePath]")]
        [Description(
            "The path of the source code to be converted. Can be either an URL or local directory path. Uses current directory by default."
        )]
        public string SourcePath { get; set; } = Directory.GetCurrentDirectory();
    }

    public override Task<int> ExecuteAsync(
        [NotNull] CommandContext context,
        [NotNull] Settings settings
    )
    {
        AnsiConsole.MarkupLineInterpolated(
            $"The specified source path is: [green]{settings.SourcePath}[/]"
        );

        return Task.FromResult(0);
    }

    public override ValidationResult Validate(
        [NotNull] CommandContext context,
        [NotNull] Settings settings
    )
    {
        if (!PathValidator.IsUrlOrDirectory(settings.SourcePath))
        {
            return ValidationResult.Error("The specified path is not valid.");
        }

        return base.Validate(context, settings);
    }
}
