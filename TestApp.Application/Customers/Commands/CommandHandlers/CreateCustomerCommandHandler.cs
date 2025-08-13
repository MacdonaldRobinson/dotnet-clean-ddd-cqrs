using System;
using MediatR;
using TestApp.Application.Customers.Dtos;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.Interfaces;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Application.Customers.Commands.CommandHandlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerReadDto>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<CustomerReadDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await _customerRepository.CreateAsync(new CreateCustomerParam()
        {
            Name = new CustomerNameValueObject(request.CustomerWriteDto.Name)
        });

        return new CustomerReadDto()
        {
            Name = customer.Name.Value,
            Id = customer.Id,
            DateCreated = customer.DateCreated,
            DateLastModified = customer.DateLastModified
        };
    }
}
