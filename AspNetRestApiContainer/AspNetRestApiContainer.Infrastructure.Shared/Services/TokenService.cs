using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetRestApiContainer.Application.Parameters.Auth;
using AspNetRestApiContainer.Application.Wrappers;
using AspNetRestApiContainer.Domain.Entities;
using AspNetRestApiContainer.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace AspNetRestApiContainer.Infrastructure.Shared.Services
{
    public static class TokenService
    {
        private const int ExpirationMinutes = 60;
        private const string Key = "C1CF4B7DC4C4175B6618DE4F55CA4";

        public static Response<AuthResponse> CreateToken(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new Response<AuthResponse>(
                new AuthResponse
                {
                    Login = user.Login,
                    Title = Enum.GetName(typeof(UserTitle), user.Title),
                    Token = tokenHandler.WriteToken(token)
                });
        }

        private static JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                "CoreIdentity",
                "CoreIdentityUser",
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private static List<Claim> CreateClaims(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim("id", user.Id.ToString()),
                    new Claim("title", ((int)user.Title).ToString())
                };
                return claims;
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        private static SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Key)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
