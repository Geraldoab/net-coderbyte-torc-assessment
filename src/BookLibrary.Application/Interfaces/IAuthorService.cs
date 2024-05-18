using BookLibrary.Core;
using BookLibrary.Core.Entities;

namespace BookLibrary.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<OperationResult<Author>> AddAsync(Author newAuthor, CancellationToken cancellationToken);

        Task<OperationResult<IReadOnlyList<Author>>> GetAllAsync();

        Task<OperationResult<Author>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<OperationResult<Author>> EditAsync(int id, Author entity, CancellationToken cancellationToken);

        Task<OperationResult<Author>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
