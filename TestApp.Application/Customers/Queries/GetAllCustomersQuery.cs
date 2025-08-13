using System;
using MediatR;
using TestApp.Application.Customers.Dtos;

namespace TestApp.Application.Customers.Queries;

public class GetAllCustomersQuery : IRequest<List<CustomerReadDto>>
{

}
