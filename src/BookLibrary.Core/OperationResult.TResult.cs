using System;

namespace BookLibrary.Core;

public class OperationResult<TResult> : OperationResult
{
    public OperationResult()
    {
        
    }
    private OperationResult(TResult result)
    {
        this.Result = result;
        this.Succeeded = true;
    }

    private OperationResult(Error errorResult)
        : base(errorResult)
    {
    }

    public TResult Result { get; protected set; }

    public static OperationResult<TResult> Error(params string[] errorMessages) => new OperationResult<TResult>(new Error
    {
        Code = ErrorCode.InternalError,
        Message = string.Join(Environment.NewLine, errorMessages)
    });
    
    public static OperationResult<TResult> NotFound(params string[] errorMessages) => new OperationResult<TResult>(new Error
    {
        Code = ErrorCode.NotFound,
        Message = string.Join(Environment.NewLine, errorMessages)
    });

    public static OperationResult<TResult> Success(TResult result) => new(result);


    public static implicit operator TResult(OperationResult<TResult> result)
    {
        return result == null ? default : result.Result;
    }

    public static implicit operator OperationResult<TResult>(TResult result)
    {
        return new OperationResult<TResult>(result);
    }
}