using BookLibrary.Core;
using BookLibrary.Core.Entities;

namespace BookLibrary.Application.Interfaces
{
    public interface IPublisherService
    {
        Task<OperationResult<Publisher>> AddAsync(Publisher newPublisher, CancellationToken cancellationToken);

        Task<OperationResult<IReadOnlyList<Publisher>>> GetAllAsync();

        Task<OperationResult<Publisher>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<OperationResult<Publisher>> EditAsync(int id, Publisher entity, CancellationToken cancellationToken);

        Task<OperationResult<Publisher>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
