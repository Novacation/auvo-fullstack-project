using System.ComponentModel.DataAnnotations;

namespace GloboClimaPlatform.Application.DTOs.User;

public class UserRegisterDto
{
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    public required string Name { get; set; }
}