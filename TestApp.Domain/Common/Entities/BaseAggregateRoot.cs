using System;
using TestApp.Domain.Common.Interfaces;

namespace TestApp.Domain.Common.Entities;

public abstract class BaseAggregateRoot<TParam, TReturn> : BaseEntiy, IAggregateRoot
where TReturn : IAggregateRoot
where TParam : ICreateAggregateRootParam
{
    private List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
