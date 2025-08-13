using System;
using MediatR;
using TestApp.Application.Customers.Dtos;
using TestApp.Domain.Customers.Entities;

namespace TestApp.Application.Customers.Commands;

public record CreateCustomerCommand : IRequest<CustomerReadDto>
{
    public required CustomerWriteDto CustomerWriteDto { get; init; }
}
