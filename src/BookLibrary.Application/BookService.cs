using BookLibrary.Application.Interfaces;
using BookLibrary.Application.Interfaces.Events;
using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Messages;
using BookLibrary.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Application
{
    public class BookService : BaseService<Book>, IBookService
    {
        protected delegate void BookAdded(Book book);
        protected event BookAdded? BookAddedNotification;

        private readonly IRepository<Book> _repository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Publisher> _publisherRepository;
        private readonly IBookNotification _bookNotification;

        public BookService(IRepository<Book> repository, IRepository<Author> authorRepository,
            IRepository<Publisher> publisherRepository, IBookNotification bookNotification) : base(repository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
            _bookNotification = bookNotification;

            BookAddedNotification += _bookNotification.OnBookAdded;
        }

        async Task<OperationResult<IReadOnlyList<Book>>> IBookService.GetAllAsync(SearchByEnum searchBy, string? searchValue, int pageNumber, 
            int pageSize, CancellationToken cancellationToken)
        {
            /* 
             * We need be careful with eager loading of related data. It can be improved also using Dapper.
             * We can also create database indexes when necessary to improve database performance
             * We need to avoid tabela scan and index scan
             * We can also add a cache layer with Redis or another database in this case I'll add local cache by
             * using [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 10)]
             */
            var bookEntity = _repository.GetEntity();

            IQueryable<Book> bookQueryable = bookEntity.AsQueryable();

            if(!string.IsNullOrEmpty(searchValue))
            {
                if (searchBy == SearchByEnum.Category)
                    bookQueryable = bookQueryable.Where(w => w.Category.ToLower().Contains(searchValue.ToLower()));
                else if(searchBy == SearchByEnum.Type)
                    bookQueryable = bookQueryable.Where(w => w.Type.ToLower().Contains(searchValue.ToLower()));
                else if (searchBy == SearchByEnum.Title)
                    bookQueryable = bookQueryable.Where(w => w.Title.ToLower().Contains(searchValue.ToLower()));
            }

            cancellationToken.ThrowIfCancellationRequested();

            if(pageNumber <=0)
                pageNumber = 1;

            if(pageSize <= 0)
                pageSize = 10;

            var books = await bookQueryable.Select(s => new Book
                {
                    Id = s.Id,
                    ISBN = s.ISBN,
                    Category = s.Category,
                    CopiesInUse = s.CopiesInUse,
                    TotalCopies = s.TotalCopies,
                    Title = s.Title,
                    Type = s.Type,
                    Author = s.Author,
                    Publisher = s.Publisher,
                })
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            if (books.Count > 0)
            {
                var totalBooks = await bookQueryable.CountAsync(cancellationToken);
                books[0].TotalItemCount = totalBooks;
            }

            return OperationResult<IReadOnlyList<Book>>.Success(books);
        }

        async Task<OperationResult<Book>> IBookService.AddAsync(Book newBook, CancellationToken cancellationToken)
        {
            if((await _repository.FindAsync(w=> w.Title == newBook.Title, cancellationToken)) is not null)
                return OperationResult<Book>.Error(ErrorCode.BadRequest, string.Format(CommonMessage.BOOK_ALREADY_ADDED, newBook.Title));

            if (newBook.AuthorId <= 0 || (await _authorRepository.GetByIdAsync(newBook.AuthorId)) == null)
                return OperationResult<Book>.NotFound(string.Format(CommonMessage.AUTHOR_NOT_FOUND, newBook.AuthorId));

            if (newBook.PublisherId <= 0 || (await _publisherRepository.GetByIdAsync(newBook.PublisherId)) == null)
                return OperationResult<Book>.NotFound(string.Format(CommonMessage.PUBLISHER_NOT_FOUND, newBook.PublisherId));

            cancellationToken.ThrowIfCancellationRequested();

            var addedBook = await _repository.AddAsync(newBook, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            BookAddedNotification?.Invoke(addedBook);

            return OperationResult<Book>.Success(addedBook);
        }

        async Task<OperationResult<Book>> IBookService.GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(id, cancellationToken);
            if (book is null)
                return OperationResult<Book>.NotFound(string.Format(CommonMessage.BOOK_NOT_FOUND, id));

            return OperationResult<Book>.Success(book);
        }

        async Task<OperationResult<Book>> IBookService.EditAsync(Book book, CancellationToken cancellationToken)
        {
            var result = await _repository.EditAsync(book.Id, book, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return OperationResult<Book>.Success(result);   
        }

        async Task<OperationResult<Book>> IBookService.DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _repository.RemoveByIdAsync(id, cancellationToken);
            if (book is null)
                return OperationResult<Book>.NotFound(string.Format(CommonMessage.BOOK_NOT_FOUND, id));

            await _repository.SaveChangesAsync(cancellationToken);

            return OperationResult<Book>.Success(book);
        }
    }
}
