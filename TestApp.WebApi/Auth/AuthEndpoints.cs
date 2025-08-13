using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TestApp.WebApi.Common;

namespace TestApp.WebApi.Auth;

public record LoginRequestDto(string UserName, string Password);

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/login", Login);
    }

    private static IResult Login(LoginRequestDto loginRequestDto, AppConfig appConfig)
    {
        if (loginRequestDto.UserName == "Mac" && loginRequestDto.Password == "Password")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginRequestDto.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var jwtConfig = appConfig.AuthConfig.JwtConfig;

            var key = new SymmetricSecurityKey(jwtConfig.KeyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // always use UTC
                signingCredentials: creds
            );

            var response = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

            return Results.Ok(response);
        }

        return Results.Unauthorized();
    }
}
