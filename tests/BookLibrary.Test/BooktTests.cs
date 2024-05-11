using AutoMapper;
using BookLibrary.Api.MappingProfiles;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using BookLibrary.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BookLibrary.Test
{
    public class BooktTests
    {
        public BooktTests()
        {
        }

        [Fact]
        public async Task Update_Succeeds()
        {
            //Arrange
            var testingContext = new BookLibraryDatabaseTestContext();
            testingContext.CreateDatabase();

            var repository = new BaseRepository<Book>(testingContext);
            var mockRepository = new Mock<IRepository<Book>>();

            var mapper = CreateIMapper();
            //Act
            //Assert
            testingContext.DisposeDatabase();
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
