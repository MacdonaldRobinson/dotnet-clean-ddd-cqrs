using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.ValueObjects;

namespace TestApp.Infrastructure.Customers.EntityTypeConfiguration;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.Name)
        .HasConversion(
            x => x.Value,
            x => new CustomerNameValueObject(x)
        )
        .IsRequired()
        .HasMaxLength(50);
    }
}
