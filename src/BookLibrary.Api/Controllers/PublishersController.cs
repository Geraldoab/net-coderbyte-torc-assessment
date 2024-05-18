using AutoMapper;
using BookLibrary.Api.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Application.Interfaces;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController(IPublisherService service, IMapper mapper) 
        : BookLibraryBaseController(mapper)
    {
        /// <summary>
        /// Get all publishers
        /// </summary>
        /// <returns>A list of publishers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublisherDataTransferObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await service.GetAllAsync();
            return MapResultToDataTransferObject<IReadOnlyList<Publisher>, IReadOnlyList<PublisherDataTransferObject>>(publishers);
        }

        /// <summary>
        /// Get an publisher by id
        /// </summary>
        /// <param name="id">The publisher id</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>The publisher</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var record = await service.GetByIdAsync(id, cancellationToken);
            return MapResultToDataTransferObject<Publisher, PublisherDataTransferObject>(record);
        }

        /// <summary>
        /// Add a new publisher
        /// </summary>
        /// <param name="request">The publisher details</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created publisher</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Add([FromBody] PublisherDataTransferObject request, CancellationToken cancellationToken)
        {
            var publisher = mapper.Map<Publisher>(request);
            publisher!.Id = 0;

            var result = await service.AddAsync(publisher, cancellationToken);
            return MapResultToDataTransferObject<Publisher, PublisherDataTransferObject>(result);
        }

        /// <summary>
        /// Update an publisher
        /// </summary>
        /// <param name="request">The publisher details</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated publisher</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Update([FromRoute(Name = "id")] int id, [FromBody] PublisherDataTransferObject request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest(CommonMessage.PUBLISHER_IS_REQUIRED);

            var publisher = mapper.Map<Publisher>(request);
            publisher!.Id = id;

            var result = await service.EditAsync(publisher.Id, publisher, cancellationToken);
            return MapResultToDataTransferObject<Publisher, PublisherDataTransferObject>(result);
        }

        /// <summary>
        /// Delete an publisher
        /// </summary>
        /// <param name="id">The publisher id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The deleted publisher</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PublisherDataTransferObject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var result = await service.DeleteAsync(id, cancellationToken);
            return MapResultToDataTransferObject<Publisher, PublisherDataTransferObject>(result);
        }
    }
}
