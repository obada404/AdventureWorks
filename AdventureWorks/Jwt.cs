using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyApp
{
    public class JwtManager
    {
        private readonly string? _secretKey;

        public JwtManager(string? secretKey)
        {
            _secretKey = secretKey;
        }

        // Generate a JWT when the user logs in
        public string GenerateJwt(string userId, string name, string email,String role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, userId),
                new Claim(JwtRegisteredClaimNames.Name, name),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: "MyApp",
                audience: "MyUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_secretKey)),
                    SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Verify the JWT when the user attempts to access a protected action
        public ClaimsPrincipal VerifyJwt(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_secretKey)),
                ValidAudience = "MyUsers",
                ValidIssuer = "MyApp"
            };

            try
            {
                var principal = new JwtSecurityTokenHandler().ValidateToken(
                    token, validationParameters, out var validatedToken);

                return principal;
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
        }
    }
}
