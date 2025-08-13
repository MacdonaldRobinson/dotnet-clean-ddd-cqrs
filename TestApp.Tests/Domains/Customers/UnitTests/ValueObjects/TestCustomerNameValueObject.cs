using System;
using TestApp.Domain.Customers.ValueObjects;
using Xunit;

namespace TestApp.Tests.Domains.Customers.UnitTests.ValueObjects;

public class TestCustomerNameValueObject
{
    [Fact]
    public void Should_Throw_Expected_Exception()
    {
        Assert.Throws<Exception>(() => new CustomerNameValueObject(""));
    }

    [Fact]
    public void Should_Return_Valid_Object()
    {
        var result = new CustomerNameValueObject("Mac");
        Assert.Equal("Mac", result.Value);
    }

}
