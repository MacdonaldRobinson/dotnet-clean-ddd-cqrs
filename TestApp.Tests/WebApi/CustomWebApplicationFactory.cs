using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TestApp.Infrastructure;
using TestApp.Domain.Customers.Entities;
using TestApp.Domain.Customers.ValueObjects;
using Microsoft.Extensions.Hosting;

namespace TestApp.Tests.WebApi;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");  // <-- Set the environment here

        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Register the In-Memory database provider for testing
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        });
    }
}
