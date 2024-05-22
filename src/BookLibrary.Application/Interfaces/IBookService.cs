using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;

namespace BookLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<IReadOnlyList<Book>>> GetAllAsync(SearchByEnum searchBy, string? searchValue, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<OperationResult<Book>> AddAsync(Book newBook, CancellationToken cancellationToken);
        Task<OperationResult<Book>> EditAsync(Book book, CancellationToken cancellationToken);
        Task<OperationResult<Book>> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<OperationResult<Book>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
