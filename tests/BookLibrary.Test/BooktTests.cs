using BookLibrary.Application;
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

namespace BookLibrary.Test
{
    public class BooktTests : BaseTest
    {
        private BookService _bookService;
        private Mock<IRepository<Book>> _mockRepository = new Mock<IRepository<Book>>();
        private Mock<IRepository<Author>> _authorMockRepository = new Mock<IRepository<Author>>();
        private Mock<IRepository<Publisher>> _publisherMockRepository = new Mock<IRepository<Publisher>>();
        private BookLibraryDatabaseTestContext _testingContext = new BookLibraryDatabaseTestContext();
        
        public BooktTests()
        {
            _testingContext.CreateDatabase();
            _mockRepository.Setup(s => s.GetEntity()).Returns(_testingContext.Set<Book>());
            _authorMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<object[]>())).ReturnsAsync(new Author());
            _publisherMockRepository.Setup(s => s.GetByIdAsync(It.IsAny<object[]>())).ReturnsAsync(new Publisher());
        }

        [Fact]
        [DisplayName("Get_all_books_successfully")]
        public async Task Get_all_books_successfully()
        {
            //Arrange
            _bookService = new BookService(_mockRepository.Object, _authorMockRepository.Object, _publisherMockRepository.Object, new BookNotification());

            //Act
            var bookList = await ((Application.Interfaces.IBookService)_bookService).GetAllAsync(SearchByEnum.All, null, 1, 10, CancellationToken.None);

            //Assert
            bookList.Should().NotBeNull();
            bookList.Succeeded.Should().BeTrue();
            bookList.Result.Count.Should().BeGreaterThan(0);
        }
    }
}
