using Microsoft.AspNetCore.Mvc;
using BookLibrary.Api.Models;

namespace BookLibrary.Api.Utils;

public class ErrorResponseActionResult : ActionResult
{
    public required ErrorResponse Result { get; set; }
}