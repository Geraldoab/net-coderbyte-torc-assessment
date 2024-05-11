using BookLibrary.Core;
using BookLibrary.Core.Entities;

namespace BookLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<IReadOnlyList<Book>>> GetAllQueryableFilter();
    }
}
