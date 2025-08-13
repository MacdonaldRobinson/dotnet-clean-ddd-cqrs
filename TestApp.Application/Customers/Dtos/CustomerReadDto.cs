using System;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Application.Customers.Dtos;

public record CustomerReadDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTimeOffset DateCreated { get; init; }
    public required DateTimeOffset DateLastModified { get; init; }
}
