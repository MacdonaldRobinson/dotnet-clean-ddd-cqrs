using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestApp.WebApi.Common;

namespace TestApp.WebApi.Auth;

public class JwtOptionsConfigurator : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtConfig _jwtConfig;

    public JwtOptionsConfigurator(AppConfig appConfig)
    {
        Console.WriteLine("JwtOptionsConfigurator called contructor!");
        _jwtConfig = appConfig.AuthConfig.JwtConfig;
    }

    public void Configure(JwtBearerOptions options)
    {
        Console.WriteLine("JwtOptionsConfigurator called Configure!");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwtConfig.Issuer,
            ValidAudience = _jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(_jwtConfig.KeyBytes)
        };
    }
}
