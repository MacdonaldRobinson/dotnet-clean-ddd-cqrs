using System;
using MediatR;

namespace TestApp.Domain.Common.Interfaces;

public interface IDomainEvent : INotification
{
    Guid EntityId { get; set; }
    DateTimeOffset OccuredOn { get; }
}
