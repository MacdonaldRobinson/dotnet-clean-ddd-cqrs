using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TestApp.Application.Customers.DomainEventHandlers;
using TestApp.Domain.Common.Interfaces;
using TestApp.Domain.Customers.DomainEvents;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.ValueObjects;
using TestApp.Infrastructure;
using TestApp.Infrastructure.Customers.Repositories;
using Xunit;

namespace TestApp.Tests.Application.Customers.DomainEventHandlers;

public class TestCustomerCreatedDomainEventHandler
{
    [Fact]
    public async Task Should_Return_Expected_Response()
    {
        var mockMediator = new Mock<IMediator>();
        var mockLogger = new Mock<ILogger<CustomerCreatedDomainEventHandler>>();

        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("Test")
        .Options;

        var dbContext = new AppDbContext(options, mockMediator.Object);

        var customerRepository = new CustomerRepository(dbContext);

        CustomerCreatedDomainEventHandler handler = new CustomerCreatedDomainEventHandler(customerRepository, mockLogger.Object);
        CreateCustomerParam createCustomerParam = new CreateCustomerParam() { Name = new CustomerNameValueObject("Mac") };
        Customer customer = Customer.Create(createCustomerParam);

        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();

        mockMediator.Verify(m => m.Publish<IDomainEvent>(
            It.Is<CustomerCreatedDomainEvent>(x => x.EntityId == customer.Id && x.EventData == createCustomerParam),
            It.IsAny<CancellationToken>()
            ), Times.Once);
    }
}
