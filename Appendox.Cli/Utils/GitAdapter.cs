using CliWrap.Buffered;
using LanguageExt;
using System.Text.RegularExpressions;

namespace Appendox.Cli.Utils;

internal static class GitAdapter
{
    private static readonly Regex _gitVersionRegex =
        new(
            @"^git version \d+\.\d+\.\d+(?:\.windows\.\d+)?$",
            RegexOptions.Compiled,
            TimeSpan.FromMilliseconds(500)
        );

    public static async Task<Either<string, string>> GetGitVersionAsync(CancellationToken ct)
    {
        var gitVersionResult = await CliWrap.Cli
            .Wrap("git")
            .WithArguments(new[] { "--version" })
            .ExecuteBufferedAsync(ct);

        if (!string.IsNullOrWhiteSpace(gitVersionResult.StandardError))
        {
            return Either<string, string>.Left(gitVersionResult.StandardError);
        }

        var trimmedVersionOutput = gitVersionResult.StandardOutput?.Trim();

        if (
            string.IsNullOrWhiteSpace(trimmedVersionOutput)
            || !_gitVersionRegex.IsMatch(trimmedVersionOutput!)
        )
        {
            return Either<string, string>.Left("Failed to define a version of installed Git.");
        }

        return Either<string, string>.Right(trimmedVersionOutput!);
    }
}
