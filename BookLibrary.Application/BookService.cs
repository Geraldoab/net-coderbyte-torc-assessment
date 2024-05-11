using BookLibrary.Application.Interfaces;
using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Application
{
    public class BookService : BaseService<Book>, IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<IReadOnlyList<Book>>> GetAllQueryableFilter()
        {
            /* 
             * We need be careful with eager loading of related data. It can be improved also using Dapper.
             * We can also create database indexes when necessary to improve database performance
             * We need to avoid tabela scan and index scan
             * We can also add a cache layer with Redis or another database in this case I'll add local cache by
             * using [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
             */
            var books = await _repository.GetEntity()
                .Include(a=> a.Author)
                .Include(P => P.Publisher)
                .Select(s => new Book
                {
                    ISBN = s.ISBN,
                    Category = s.Category,
                    CopiesInUse = s.CopiesInUse,
                    TotalCopies = s.TotalCopies,
                    Title = s.Title,
                    Type = s.Type,
                    Author = s.Author,
                    Publisher = s.Publisher,
                }).ToListAsync();
         
            return OperationResult<IReadOnlyList<Book>>.Success(books);
        }
    }
}
