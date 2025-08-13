using System;
using TestApp.Domain.Common.Interfaces;
using TestApp.Domain.Customers.Entities;

namespace TestApp.Domain.Customers.Interfaces;

public interface ICustomerRepository : IRepository<CreateCustomerParam, Customer>
{

}
