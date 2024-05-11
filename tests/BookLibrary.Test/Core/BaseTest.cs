using AutoMapper;
using BookLibrary.Api.MappingProfiles;

namespace BookLibrary.Test.Core
{
    public abstract class BaseTest
    {
        protected virtual IMapper CreateIMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataTransferObjectsMappingProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
