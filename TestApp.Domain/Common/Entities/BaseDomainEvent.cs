using System;
using TestApp.Domain.Common.Interfaces;

namespace TestApp.Domain.Common.Entities;

public abstract record BaseDomainEvent<T> : IDomainEvent
{
    public Guid EntityId { get; set; }
    public DateTimeOffset OccuredOn { get; } = DateTimeOffset.UtcNow;

    public required T EventData { get; init; }
}
