using System;
using TestApp.Domain.Common.Interfaces;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Domain.Customers.Entities;

public record CreateCustomerParam : ICreateAggregateRootParam
{
    public required CustomerNameValueObject Name { get; init; }

}
