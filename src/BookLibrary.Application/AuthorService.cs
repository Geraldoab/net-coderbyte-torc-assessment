using BookLibrary.Application.Interfaces;
using BookLibrary.Core;
using BookLibrary.Core.Entities;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Messages;
using BookLibrary.Core.Services;

namespace BookLibrary.Application
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        private readonly IRepository<Author> _repository;
        public AuthorService(IRepository<Author> repository) : base(repository)
        {
            _repository = repository;
        }

        async Task<OperationResult<Author>> IAuthorService.AddAsync(Author newAuthor, CancellationToken cancellationToken)
        {
            if((await _repository.FindAsync(p=> string.Equals(p.Name, newAuthor.Name, StringComparison.OrdinalIgnoreCase), cancellationToken) is not null))
                return OperationResult<Author>.Error(ErrorCode.BadRequest, string.Format(CommonMessage.AUTHOR_ALREADY_ADDED, newAuthor.Name));

            return await base.AddAsync(newAuthor, cancellationToken);
        }
    }
}
