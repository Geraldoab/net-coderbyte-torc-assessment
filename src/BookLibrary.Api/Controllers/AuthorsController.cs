using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
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
    }
}
