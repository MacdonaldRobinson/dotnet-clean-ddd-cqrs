using System;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Application.Customers.Dtos;

public record CustomerWriteDto
{
    public required string Name { get; init; }
}
