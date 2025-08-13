using System;

namespace TestApp.Domain.Common.Interfaces;

public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
