using System;
using TestApp.Domain.Common.Interfaces;

namespace TestApp.Domain.Common.Entities;

public abstract class BaseEntiy : IEntity
{
    public Guid Id { get; set; }

    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset DateLastModified { get; set; } = DateTimeOffset.UtcNow;
}
