using System;
using System.Data.Common;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestApp.Domain.Common.Entities;
using TestApp.Domain.Common.Interfaces;
using TestApp.Domain.Customers.Entities;
using TestApp.Infrastructure.Customers.EntityTypeConfiguration;

namespace TestApp.Infrastructure;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityTypeConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int response = await base.SaveChangesAsync(cancellationToken);

            if (response > 0)
            {
                var entities = ChangeTracker.Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

                foreach (var entity in entities)
                {
                    foreach (var domainEvent in entity.DomainEvents)
                    {
                        domainEvent.EntityId = entity.Id;
                        await _mediator.Publish(domainEvent);
                    }

                    entity.ClearDomainEvents();
                }
            }
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DbSet<Customer> Customers { get; set; }
}
