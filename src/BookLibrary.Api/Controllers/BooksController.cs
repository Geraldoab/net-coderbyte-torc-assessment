using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;

namespace BookLibrary.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController(IService<Book> bookService,
    IRepository<Book> bookRepository,
    IMapper mapper)
    : BookLibraryBaseController(mapper)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var books = await bookService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Book>, IReadOnlyList<BookDataTransferObject>>(books);
    }
}