using BookLibrary.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Repositories;

public class BaseRepository<TEntity>(BookLibraryDbContext context)
    : IRepository<TEntity>
    where TEntity : class
{
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public DbSet<TEntity> GetEntity()
    {
        return context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity> GetByIdAsync(params object[] keyValues)
    {
        return await context.Set<TEntity>().FindAsync(keyValues);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<TEntity> EditAsync(object id, TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            return null;

        TEntity? selectedEntity = await GetByIdAsync(id, cancellationToken);

        if (selectedEntity != null)
            context.Entry(selectedEntity).CurrentValues.SetValues(entity);

        return selectedEntity;
    }

    public async Task<TEntity> RemoveByIdAsync(object id, CancellationToken cancellationToken)
    {
        var selectedEntity = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (selectedEntity != null)
            context.Set<TEntity>().Remove(selectedEntity);

        return selectedEntity;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
}