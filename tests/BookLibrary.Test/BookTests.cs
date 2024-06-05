using BookLibrary.Application;
using BookLibrary.Application.Interfaces;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Enums;
using BookLibrary.Core.Interfaces;
using BookLibrary.Test.Core;
using FluentAssertions;
using Moq;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq.Expressions;
using System;

namespace BookLibrary.Test
{
    public class BookTests : BaseTest
    {
        private BookService _bookService;
        private Mock<IRepository<Book>> _bookRepository = new Mock<IRepository<Book>>();
        private Mock<IRepository<Author>> _authorMockRepository = new Mock<IRepository<Author>>();
        private Mock<IRepository<Publisher>> _publisherMockRepository = new Mock<IRepository<Publisher>>();
        private BookLibraryDatabaseTestContext _testingContext = new BookLibraryDatabaseTestContext();
        private readonly Book _newBook = new Book()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            ISBN = "Test",
            Category = "Test",
            CopiesInUse = 0,
            Title = "Pride and Prejudice",
            TotalCopies = 50,
            Type = "Test",
            AuthorId = 1,
            PublisherId = 1,
        };

        public BookTests()
        {
            _testingContext.CreateDatabase();
            _bookRepository.Setup(s => s.GetEntity()).Returns(_testingContext.Set<Book>());
            _bookRepository.Setup(s => s.FindAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<Book>());
            _bookRepository.Setup(s => s.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_newBook);
            _bookRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_newBook);
            _bookRepository.Setup(s => s.EditAsync(It.IsAny<object>(), It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_newBook);
            _bookRepository.Setup(s => s.RemoveByIdAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_newBook);

            _bookRepository.Setup(s => s.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            _authorMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<object[]>())).ReturnsAsync(new Author());
            _publisherMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<object[]>())).ReturnsAsync(new Publisher());
        }

        [Fact]
        [DisplayName("Get_all_books_successfully")]
        public async Task Get_all_books_successfully()
        {
            //Arrange
            _bookService = new BookService(_bookRepository.Object, _authorMockRepository.Object, _publisherMockRepository.Object, new BookNotification());

            //Act
            var bookList = await ((IBookService)_bookService).GetAllAsync(SearchByEnum.All, null, 1, 10, CancellationToken.None);

            //Assert
            _bookRepository.Verify(m => m.GetEntity(), Times.Exactly(1));

            bookList.Should().NotBeNull();
            bookList.Succeeded.Should().BeTrue();
            bookList.Result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        [DisplayName("Add_a_book_successfully")]
        public async Task Add_a_book_successfully()
        {
            //Arrange
            _bookService = new BookService(_bookRepository.Object, _authorMockRepository.Object, _publisherMockRepository.Object, new BookNotification());

            //Act
            var newBook = await ((IBookService)_bookService).AddAsync(_newBook, CancellationToken.None);

            //Assert
            _bookRepository.Verify(m => m.FindAsync(It.IsAny<Expression<Func<Book, bool>>>(), CancellationToken.None), Times.Exactly(1));
            _authorMockRepository.Verify(m => m.GetByIdAsync(1), Times.Exactly(1));
            _publisherMockRepository.Verify(m => m.GetByIdAsync(1), Times.Exactly(1));

            _bookRepository.Verify(m => m.AddAsync(_newBook, CancellationToken.None), Times.Exactly(1));
            _bookRepository.Verify(m=> m.SaveChangesAsync(CancellationToken.None), Times.Exactly(1));

            newBook.Should().NotBeNull();
            newBook.Succeeded.Should().BeTrue();
            newBook.Result.Id.Should().BeGreaterThan(0);
        }
    }
}
