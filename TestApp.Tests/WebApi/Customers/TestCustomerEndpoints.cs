using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TestApp.Application.Customers.Dtos;
using TestApp.Domain.Customers.Entities;
using TestApp.WebApi.Auth;
using Xunit;

namespace TestApp.Tests.WebApi.Customers;

public class TestCustomerEndpoints : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _customWebApplicationFactory;

    public TestCustomerEndpoints(CustomWebApplicationFactory customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;
    }

    [Fact]
    public async Task Should_Return_Expected_Response()
    {
        var customers = await GetAllCustomers();
        Assert.NotNull(customers);
        Assert.True(customers.Count == 0);

        var customer = await CreateCustomer(new CustomerWriteDto() { Name = "Mac" });
        Assert.NotNull(customer);
        Assert.Equal("Mac", customer.Name);

        var newCustomers = await GetAllCustomers();
        Assert.NotNull(newCustomers);
        Assert.True(newCustomers.Count == 1);
    }

    public async Task<List<CustomerReadDto>?> GetAllCustomers()
    {
        var httpClient = _customWebApplicationFactory.CreateClient();
        var response = await httpClient.GetAsync("/api/customers");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<CustomerReadDto>>();
    }

    public async Task<LoginResponseDto?> GetToken()
    {
        var httpClient = _customWebApplicationFactory.CreateClient();
        var response = await httpClient.PostAsJsonAsync("/api/login", new LoginRequestDto("Mac", "Password"));
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }

    public async Task<CustomerReadDto?> CreateCustomer(CustomerWriteDto customerWriteDto)
    {
        var httpClient = _customWebApplicationFactory.CreateClient();

        var loginResponseDto = await GetToken();
        Assert.NotNull(loginResponseDto);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginResponseDto.Token}");

        var response = await httpClient.PostAsJsonAsync("/api/customers", customerWriteDto);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CustomerReadDto>();
    }
}
