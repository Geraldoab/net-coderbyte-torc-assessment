using BookLibrary.Core;

namespace BookLibrary.Api.Models;

public class ErrorResponse
{
    public required Error Error { get; set; }
}