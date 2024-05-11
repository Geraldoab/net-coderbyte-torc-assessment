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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await context.SaveChangesAsync(cancellationToken);
}