using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BookLibrary.Core;

namespace BookLibrary.Api.Utils;

public class ErrorResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ErrorResponseActionResult casted)
        {
            context.Result = new ObjectResult(casted.Result)
            {
                StatusCode = casted.Result.Error.Code switch
                {
                    ErrorCode.InternalError => StatusCodes.Status500InternalServerError,
                    ErrorCode.NotFound => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status400BadRequest
                },
            };
        }

        await next();
    }
}