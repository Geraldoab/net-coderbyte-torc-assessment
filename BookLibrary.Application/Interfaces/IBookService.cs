using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;

namespace BookLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<OperationResult<IReadOnlyList<Book>>> GetAllQueryableFilter(SearchByEnum searchBy, string? searchValue);
    }
}
