using CliFx.Extensibility;
using System.IO;

namespace Appendox.Cli.Validators;

internal sealed class PathValidator : BindingValidator<string>
{
    public override BindingValidationError? Validate(string? value)
    {
        if (value is null)
        {
            return Error("The path was not specified.");
        }

        var isUrl =
            value.StartsWith("https://")
            || value.StartsWith("http://")
            || Uri.TryCreate(value, UriKind.Absolute, out _);
        var isDirectory = Directory.Exists(value);
        if (!isUrl && !isDirectory)
        {
            return Error("The specified path is invalid.");
        }

        return Ok();
    }
}
