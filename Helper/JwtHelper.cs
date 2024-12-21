using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Bag_E_Commerce.Models;

namespace Bag_E_Commerce.Helper
{
    public static class JwtHelper
    {
        private static string SecretKey = "welcometoegdkmanglorewelcometoegdkmanglorewelcometoegdkmanglore"; // Use a more secure key in production

        public static string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "yourapp.com",
                    ValidAudience = "yourapp.com",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out var validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
