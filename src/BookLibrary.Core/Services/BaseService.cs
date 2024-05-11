using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookLibrary.Core.Interfaces;

namespace BookLibrary.Core.Services;

public class BaseService<TEntity>(IRepository<TEntity> repository) : IService<TEntity>
    where TEntity : class
{
    public async Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        return OperationResult<IReadOnlyList<TEntity>>.Success(await repository.GetAllAsync());
    }

    public async Task<OperationResult<IReadOnlyList<TEntity>>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return OperationResult<IReadOnlyList<TEntity>>.Success(await repository.FilterAsync(predicate));
    }
}