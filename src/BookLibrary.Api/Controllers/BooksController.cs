using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Application.Interfaces;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;
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
    /// <param name="searchBy">The filter type</param>
    /// <param name="searchValue">The filter value</param>
    /// <returns>A list of books</returns>
    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 10)]
    [ProducesResponseType(typeof(IEnumerable<BookSearchDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetAll(SearchByEnum searchBy, string? searchValue, CancellationToken cancellationToken)
    {
        var books = await bookService.GetAllAsync(searchBy, searchValue, cancellationToken);
        return MapResultToDataTransferObject<IReadOnlyList<Book>, IReadOnlyList<BookSearchDataTransferObject>>(books);
    }

    /// <summary>
    /// Add a new book
    /// </summary>
    /// <param name="request">The book details</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The created book</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Add([FromBody] BookDataTransferObject request, CancellationToken cancellationToken)
    {
        var result = await bookService.AddAsync(mapper.Map<Book>(request)!, cancellationToken);
        return MapResultToDataTransferObject<Book, BookDataTransferObject>(result);
    }
}