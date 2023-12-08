namespace Appendox.Core.Exceptions;

public class GitCliException : Exception
{
    public GitCliException(string message)
        : base(message) { }
}
