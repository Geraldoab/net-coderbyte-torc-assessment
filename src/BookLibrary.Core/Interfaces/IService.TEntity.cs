using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Core.Interfaces;

public interface IService<TEntity>
    where TEntity : class
{
    Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync();

    Task<OperationResult<IReadOnlyList<TEntity>>> FilterAsync(Expression<Func<TEntity, bool>> predicate);

    Task<OperationResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<OperationResult<TEntity>> EditAsync(int id, TEntity entity, CancellationToken cancellationToken);

    Task<OperationResult<TEntity>> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<OperationResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken);
}