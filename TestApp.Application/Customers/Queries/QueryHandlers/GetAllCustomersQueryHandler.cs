using System;
using MediatR;
using TestApp.Application.Customers.Dtos;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.Interfaces;

namespace TestApp.Application.Customers.Queries.QueryHandlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerReadDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerReadDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await _customerRepository.GetAllAsync();

        List<CustomerReadDto> customersReadDtos = customers.Select(x => new CustomerReadDto()
        {
            Name = x.Name.Value,
            DateCreated = x.DateCreated,
            DateLastModified = x.DateLastModified,
            Id = x.Id
        })
        .ToList();

        return customersReadDtos;

    }
}
