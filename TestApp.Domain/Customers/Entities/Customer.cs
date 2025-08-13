using TestApp.Domain.Common.Entities;
using TestApp.Domain.Customers.DomainEvents;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Domain.Customers.Entities;

public class Customer : BaseAggregateRoot<CreateCustomerParam, Customer>
{
    private Customer() { }
    public required CustomerNameValueObject Name { get; init; }

    public static Customer Create(CreateCustomerParam param)
    {
        Customer customer = new Customer()
        {
            Name = param.Name
        };

        var domainEvent = new CustomerCreatedDomainEvent()
        {
            EventData = param
        };

        customer.AddDomainEvent(domainEvent);

        return customer;
    }
}
