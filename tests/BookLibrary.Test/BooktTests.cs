using AutoMapper;
using BookLibrary.Api.MappingProfiles;
using BookLibrary.Application;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using FluentAssertions;
using Moq;
using System.ComponentModel;
using System.Threading.Tasks;
using Xunit;

namespace BookLibrary.Test
{
    public class BooktTests
    {
        private BookService _bookService;
        private Mock<IRepository<Book>> _mockRepository = new Mock<IRepository<Book>>();
        private BookLibraryDatabaseTestContext _testingContext = new BookLibraryDatabaseTestContext();
        
        public BooktTests()
        {
            _testingContext.CreateDatabase();
            _mockRepository.Setup(s => s.GetEntity()).Returns(_testingContext.Set<Book>());
        }

        [Fact]
        [DisplayName("Get_all_books_successfully")]
        public async Task Get_all_books_successfully()
        {
            //Arrange
            _bookService = new BookService(_mockRepository.Object);

            //Act
            var bookList = await _bookService.GetAllQueryableFilter();

            //Assert
            bookList.Should().NotBeNull();
            bookList.Succeeded.Should().BeTrue();
            bookList.Result.Count.Should().BeGreaterThan(0);
        }

        protected IMapper CreateIMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataTransferObjectsMappingProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
