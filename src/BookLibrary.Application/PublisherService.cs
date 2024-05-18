using BookLibrary.Application.Interfaces;
using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Messages;
using BookLibrary.Core.Services;

namespace BookLibrary.Application
{
    public class PublisherService : BaseService<Publisher>, IPublisherService
    {
        private readonly IRepository<Publisher> _repository;
        public PublisherService(IRepository<Publisher> repository) : base(repository)
        {
            _repository = repository;
        }

        async Task<OperationResult<Publisher>> IPublisherService.AddAsync(Publisher newPublisher, CancellationToken cancellationToken)
        {
            if ((await _repository.FindAsync(p => string.Equals(p.Name, newPublisher.Name, StringComparison.OrdinalIgnoreCase), cancellationToken) is not null))
                return OperationResult<Publisher>.Error(ErrorCode.BadRequest, string.Format(CommonMessage.PUBLISHER_ALREADY_ADDED, newPublisher.Name));

            return await base.AddAsync(newPublisher, cancellationToken);
        }
    }
}
