using System;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.ValueObjects;
using Xunit;

namespace TestApp.Tests.Domains.Customers.UnitTests.Entities;

public class TestCustomer
{
    [Fact]
    public void Create_Should_Return_Valid_Customer()
    {
        var createCustomerParam = new CreateCustomerParam()
        {
            Name = new CustomerNameValueObject("Mac")
        };

        Customer customer = Customer.Create(createCustomerParam);

        Assert.Equal("Mac", customer.Name.Value);
        Assert.Single(customer.DomainEvents);
    }

    [Fact]
    public void Create_Should_Throw_Exception()
    {
        Assert.Throws<Exception>(() => new CustomerNameValueObject(""));
    }
}
