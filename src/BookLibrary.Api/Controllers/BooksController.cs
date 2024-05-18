using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Application.Interfaces;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;
using BookLibrary.Core.Messages;
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
    [ProducesResponseType(typeof(IEnumerable<BookSearchDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetAll(SearchByEnum searchBy, string? searchValue, CancellationToken cancellationToken)
    {
        var books = await bookService.GetAllAsync(searchBy, searchValue, cancellationToken);
        return MapResultToDataTransferObject<IReadOnlyList<Book>, IReadOnlyList<BookSearchDataTransferObject>>(books);
    }

    /// <summary>
    /// Get a book by id
    /// </summary>
    /// <param name="id">The book id</param>
    /// <param name="cancellationToken">The cancellation token for the request</param>
    /// <returns>The book</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookSearchDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BookSearchDataTransferObject), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var book = await bookService.GetByIdAsync(id, cancellationToken);
        return MapResultToDataTransferObject<Book, BookDataTransferObject>(book);
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

    /// <summary>
    /// Update a new book
    /// </summary>
    /// <param name="request">The book details</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The updated book</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BookDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Update([FromRoute(Name = "id")] int id, [FromBody] BookDataTransferObject request, CancellationToken cancellationToken)
    {
        if (request == null)
            return BadRequest(CommonMessage.BOOK_IS_REQUIRED);

        var book = mapper.Map<Book>(request);
        book!.Id = id;

        var result = await bookService.EditAsync(book, cancellationToken);
        return MapResultToDataTransferObject<Book, BookDataTransferObject>(result);
    }

    /// <summary>
    /// Delete a book
    /// </summary>
    /// <param name="id">The book id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The deleted book</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(BookDataTransferObject), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BookSearchDataTransferObject), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id, CancellationToken cancellationToken)
    {
        var result = await bookService.DeleteAsync(id, cancellationToken);
        return MapResultToDataTransferObject<Book, BookDataTransferObject>(result);
    }
}