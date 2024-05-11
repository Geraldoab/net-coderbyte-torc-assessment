namespace BookLibrary.Core;

public class OperationResult
{
    protected OperationResult()
    {
        this.Succeeded = false;
    }

    private OperationResult(bool succeeded)
        : this()
    {
        this.Succeeded = succeeded;
    }

    protected OperationResult(Error errorResult)
        : this()
    {
        this.ErrorResult = errorResult;
    }

    public bool Succeeded { get; protected init; }

    public Error ErrorResult { get; init; }

    public static implicit operator bool(OperationResult value)
    {
        return value.Succeeded;
    }

    public static implicit operator OperationResult(bool value)
    {
        return new OperationResult(value);
    }
}