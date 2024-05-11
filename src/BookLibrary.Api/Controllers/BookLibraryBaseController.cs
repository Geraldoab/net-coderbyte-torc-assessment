using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

public class BookLibraryBaseController(IMapper mapper) : ControllerBase
{
    protected IActionResult MapResultToDataTransferObject<TResult, TDataTransferObject>(OperationResult<TResult> result)
    {
        if (!result.Succeeded)
        {
            if (result.ErrorResult.Code == ErrorCode.BadRequest)
                return BadRequest(result.ErrorResult.Message);
            else
            {
                return new ErrorResponseActionResult
                {
                    Result = new ErrorResponse
                    {
                        Error = new Error
                        {
                            Code = result.ErrorResult.Code,
                            Message = result.ErrorResult.Message
                        }
                    }
                };
            }
        }

        return Ok(mapper.Map<TResult, TDataTransferObject>(result));
    }
}