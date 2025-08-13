using System;

namespace TestApp.Domain.Common.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    DateTimeOffset DateCreated { get; }
    DateTimeOffset DateLastModified { get; set; }
}
