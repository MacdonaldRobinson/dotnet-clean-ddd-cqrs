using System;

namespace TestApp.Domain.Common.Interfaces;

public interface IRepository<TParam, TReturn>
where TParam : ICreateAggregateRootParam
where TReturn : IAggregateRoot
{
    Task<List<TReturn>> GetAllAsync();
    Task<TReturn?> GetByIdAsync(Guid id);
    Task<int> DeleteAsync(Guid id);
    Task<TReturn> CreateAsync(TParam param);
}
