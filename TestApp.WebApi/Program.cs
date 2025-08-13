using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestApp.Application.Customers.Commands;
using TestApp.Domain.Customers.Interfaces;
using TestApp.Infrastructure;
using TestApp.Infrastructure.Customers.Repositories;
using TestApp.WebApi.Auth;
using TestApp.WebApi.Common;
using TestApp.WebApi.Customers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AppConfig>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.Events = new JwtBearerEvents
           {
               OnAuthenticationFailed = ctx =>
               {
                   Console.WriteLine("JWT failed: " + ctx.Exception);
                   return Task.CompletedTask;
               },
               OnTokenValidated = ctx =>
               {
                   Console.WriteLine("JWT validated for: " + ctx.Principal.Identity.Name);
                   return Task.CompletedTask;
               }
           };

           var jwtConfig = builder.Configuration.GetSection("AuthConfig:JwtConfig").Get<JwtConfig>()!;

           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = jwtConfig.Issuer,
               ValidAudience = jwtConfig.Audience,
               IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.KeyBytes)
           };
       });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", options => options.RequireRole("Admin"));
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).Assembly));

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AppDatabase"))
    );
}

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/testauth", () => "OK").RequireAuthorization();

app.MapAuthEndpoints();
app.MapCustomerEndpoints();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }