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
    public class PublishersController(IService<Publisher> service, IMapper mapper) 
        : BookLibraryBaseController(mapper)
    {
        /// <summary>
        /// Get all publishers
        /// </summary>
        /// <returns>A list of publishers</returns>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 20)]
        [ProducesResponseType(typeof(IEnumerable<PublisherDataTransferObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await service.GetAllAsync();
            return MapResultToDataTransferObject<IReadOnlyList<Publisher>, IReadOnlyList<PublisherDataTransferObject>>(publishers);
        }
    }
}
