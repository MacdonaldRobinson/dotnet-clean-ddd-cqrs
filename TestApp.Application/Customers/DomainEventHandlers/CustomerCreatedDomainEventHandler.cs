using System;
using MediatR;
using Microsoft.Extensions.Logging;
using TestApp.Domain.Common.Interfaces;
using TestApp.Domain.Customers.DomainEvents;
using TestApp.Domain.Customers.Interfaces;

namespace TestApp.Application.Customers.DomainEventHandlers;

public class CustomerCreatedDomainEventHandler : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerCreatedDomainEventHandler> _logger;

    public CustomerCreatedDomainEventHandler(ICustomerRepository customerRepository, ILogger<CustomerCreatedDomainEventHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(notification.EntityId);
        _logger.LogInformation("customer {@customer}", customer);
    }
}
