using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Application.Interfaces;
using BookLibrary.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController(IBookService bookService,
    IMapper mapper)
    : BookLibraryBaseController(mapper)
{
    /// <summary>
    /// Get all books
    /// </summary>
    /// <returns>A list of books</returns>
    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
    [ProducesResponseType(typeof(IEnumerable<BookDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetAll()
    {
        var books = await bookService.GetAllQueryableFilter();
        return MapResultToDataTransferObject<IReadOnlyList<Book>, IReadOnlyList<BookDataTransferObject>>(books);
    }
}