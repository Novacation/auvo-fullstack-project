using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GloboClimaPlatform.Infra.Entities;

[Table("Users")]
public class UsersEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required, MaxLength(70)] public string Name { get; init; } = string.Empty;

    [Required, MaxLength(70)] public string Email { get; init; } = string.Empty;

    [Required, MaxLength(20)] public string Password { get; init; } = string.Empty;
    
    [Required, MaxLength(200)] public string JWT { get; set; } = string.Empty;
}