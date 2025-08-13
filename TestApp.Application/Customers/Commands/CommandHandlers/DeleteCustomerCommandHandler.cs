using System;
using MediatR;
using TestApp.Domain.Customers.Interfaces;

namespace TestApp.Application.Customers.Commands.CommandHandlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerRepository.DeleteAsync(request.Id);
    }
}
