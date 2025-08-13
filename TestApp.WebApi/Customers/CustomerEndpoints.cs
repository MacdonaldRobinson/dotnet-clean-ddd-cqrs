using System;
using System.Threading.Tasks;
using MediatR;
using TestApp.Application.Customers.Commands;
using TestApp.Application.Customers.Dtos;
using TestApp.Application.Customers.Queries;
using TestApp.Domain.Customers.Entities;

namespace TestApp.WebApi.Customers;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/api/customers", GetAllCustomers);
        routeBuilder.MapPost("/api/customers", CreateCustomer).RequireAuthorization();
        routeBuilder.MapDelete("/api/customers/{customerId:guid}", DeleteCustomer).RequireAuthorization("AdminOnly");
    }

    private static async Task<IResult> GetAllCustomers(IMediator mediator)
    {
        List<CustomerReadDto> response = await mediator.Send(new GetAllCustomersQuery());

        return Results.Ok(response);
    }

    private static async Task<IResult> CreateCustomer(IMediator mediator, CustomerWriteDto customerWriteDto)
    {
        CustomerReadDto response = await mediator.Send(new CreateCustomerCommand() { CustomerWriteDto = customerWriteDto });

        return Results.Ok(response);
    }

    private static async Task<IResult> DeleteCustomer(IMediator mediator, Guid customerId)
    {
        int response = await mediator.Send(new DeleteCustomerCommand() { Id = customerId });

        return Results.Ok(response);
    }
}
