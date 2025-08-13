using System;
using TestApp.Domain.Common.Entities;
using TestApp.Domain.Customers.Entities;

namespace TestApp.Domain.Customers.DomainEvents;

public record CustomerCreatedDomainEvent : BaseDomainEvent<CreateCustomerParam>
{

}
