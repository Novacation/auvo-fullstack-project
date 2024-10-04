using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GloboClimaPlatform.Core.UseCases.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace GloboClimaPlatform.Application.Services.Jwt;

public class GenerateJwtUseCase(IConfiguration configuration) : IGenerateJwtUseCase
{
    public string Execute(string email, string name)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Name, name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"]!,
            audience: configuration["Jwt:Audience"]!,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);    }
}