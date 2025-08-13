using System;
using TestApp.Domain.Common.Interfaces;

namespace TestApp.Infrastructure.Common.Repository;

public abstract class BaseRepository<TParam, TReturn> : IRepository<TParam, TReturn>
where TParam : ICreateAggregateRootParam
where TReturn : IAggregateRoot
{
    protected AppDbContext _dbContext { get; private set; }

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public abstract Task<List<TReturn>> GetAllAsync();

    public abstract Task<TReturn?> GetByIdAsync(Guid id);

    public abstract Task<int> DeleteAsync(Guid id);

    public abstract Task<TReturn> CreateAsync(TParam param);
}
