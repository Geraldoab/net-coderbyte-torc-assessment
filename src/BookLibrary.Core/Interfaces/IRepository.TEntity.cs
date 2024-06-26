﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookLibrary.Core.Interfaces;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task<IReadOnlyList<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    IQueryable<TEntity> GetQueryable();

    DbSet<TEntity> GetEntity();

    Task<TEntity> GetByIdAsync(params object[] keyValues);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> EditAsync(object id, TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> RemoveByIdAsync(object id, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}