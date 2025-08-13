using System;
using MediatR;

namespace TestApp.Application.Customers.Commands;

public record DeleteCustomerCommand : IRequest<int>
{
    public required Guid Id { get; init; }
}
