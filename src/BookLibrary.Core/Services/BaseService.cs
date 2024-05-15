using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task<OperationResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var addedEntity = await repository.AddAsync(entity, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        await repository.SaveChangesAsync(cancellationToken);

        return OperationResult<TEntity>.Success(addedEntity);
    }

    public async Task<OperationResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return OperationResult<TEntity>.NotFound(string.Format(CommonMessage.RECORD_NOT_FOUND, id));

        return OperationResult<TEntity>.Success(entity);
    }

    public async Task<OperationResult<TEntity>> EditAsync(int id, TEntity entity, CancellationToken cancellationToken)
    {
        var result = await repository.EditAsync(id, entity, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        await repository.SaveChangesAsync(cancellationToken);

        return OperationResult<TEntity>.Success(result);
    }

    public async Task<OperationResult<TEntity>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.RemoveByIdAsync(id, cancellationToken);
        if (entity is null)
            return OperationResult<TEntity>.NotFound(string.Format(CommonMessage.RECORD_NOT_FOUND, id));
        
        cancellationToken.ThrowIfCancellationRequested();

        await repository.SaveChangesAsync(cancellationToken);

        return OperationResult<TEntity>.Success(entity);
    }
}