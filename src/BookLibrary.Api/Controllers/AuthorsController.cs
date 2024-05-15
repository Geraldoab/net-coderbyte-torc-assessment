using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IService<Author> service, IMapper mapper) 
        : BookLibraryBaseController(mapper)
    {
        /// <summary>
        /// Get all authors
        /// </summary>
        /// <returns>A list of authors</returns>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 20)]
        [ProducesResponseType(typeof(IEnumerable<AuthorDataTransferObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> GetAll()
        {
            var authors = await service.GetAllAsync();
            return MapResultToDataTransferObject<IReadOnlyList<Author>, IReadOnlyList<AuthorDataTransferObject>>(authors);
        }

        /// <summary>
        /// Get an author by id
        /// </summary>
        /// <param name="id">The author id</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>The Author</returns>
        [HttpGet("{id:int}")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 20)]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await service.GetByIdAsync(id, cancellationToken);
            return MapResultToDataTransferObject<Author, AuthorDataTransferObject>(record);
        }

        /// <summary>
        /// Add a new Author
        /// </summary>
        /// <param name="request">The author details</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created author</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Add([FromBody] AuthorDataTransferObject request, CancellationToken cancellationToken)
        {
            var author = mapper.Map<Author>(request);
            author!.Id = 0;

            var result = await service.AddAsync(author, cancellationToken);
            return MapResultToDataTransferObject<Author, AuthorDataTransferObject>(result);
        }

        /// <summary>
        /// Update an Author
        /// </summary>
        /// <param name="request">The author details</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated author</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Update([FromRoute(Name = "id")] int id, [FromBody] AuthorDataTransferObject request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest(CommonMessage.AUTHOR_IS_REQUIRED);

            var author = mapper.Map<Author>(request);
            author!.Id = id;

            var result = await service.EditAsync(author.Id, author, cancellationToken);
            return MapResultToDataTransferObject<Author, AuthorDataTransferObject>(result);
        }

        /// <summary>
        /// Delete an Author
        /// </summary>
        /// <param name="id">The Author id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The deleted author</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthorDataTransferObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var result = await service.DeleteAsync(id, cancellationToken);
            return MapResultToDataTransferObject<Author, AuthorDataTransferObject>(result);
        }
    }
}
