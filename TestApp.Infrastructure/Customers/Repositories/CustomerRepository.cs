using System;
using Microsoft.EntityFrameworkCore;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.Interfaces;
using TestApp.Infrastructure.Common.Repository;

namespace TestApp.Infrastructure.Customers.Repositories;

public class CustomerRepository : BaseRepository<CreateCustomerParam, Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
    public override async Task<Customer> CreateAsync(CreateCustomerParam param)
    {
        bool existsInDb = _dbContext.Customers.Any(x => x.Name == param.Name);

        if (existsInDb)
        {
            throw new Exception("User with the same name already exists in the database");
        }

        var customer = Customer.Create(new CreateCustomerParam()
        {
            Name = param.Name
        });

        await _dbContext.Customers.AddAsync(customer);

        await _dbContext.SaveChangesAsync();

        return customer;
    }

    public override async Task<int> DeleteAsync(Guid id)
    {
        Customer? foundEntity = await _dbContext.Customers.FindAsync(id);

        if (foundEntity is null)
        {
            throw new Exception($"Cannot find customer with the given id '{id}'");
        }

        _dbContext.Customers.Remove(foundEntity);

        return await _dbContext.SaveChangesAsync();
    }

    public override async Task<List<Customer>> GetAllAsync()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public override async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Customers.FindAsync(id);
    }
}
