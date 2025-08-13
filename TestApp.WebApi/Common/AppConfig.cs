using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TestApp.WebApi.Common;

public record JwtConfig(string Key, string Issuer, string Audience)
{
    // Always decode base64 key
    public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
};

public record AuthConfig(JwtConfig JwtConfig);

public class AppConfig
{
    public AuthConfig AuthConfig { get; }

    public AppConfig(IConfiguration configuration)
    {
        Console.WriteLine("AppConfig was called");
        AuthConfig = configuration.GetSection("AuthConfig")
                      .Get<AuthConfig>()
                      ?? throw new InvalidOperationException("Missing Auth configuration");
    }
}
