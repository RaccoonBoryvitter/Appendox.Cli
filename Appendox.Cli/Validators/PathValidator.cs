namespace Appendox.Cli.Validators;

internal static class PathValidator
{
    public static bool IsUrlOrDirectory(string path)
    {
        var isUrl =
            path.StartsWith("https://")
            || path.StartsWith("http://")
            || Uri.TryCreate(path, UriKind.Absolute, out _);
        var isDirectory = Directory.Exists(path);
        return isUrl || isDirectory;
    }
}
