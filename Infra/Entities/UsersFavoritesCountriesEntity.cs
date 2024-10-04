using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GloboClimaPlatform.Infra.Entities;

[PrimaryKey("Id")]
[Table("UsersFavoritesCountries")]
public class UsersFavoritesCountriesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public int UserId { get; init; }

    [ForeignKey(nameof(UserId))] public UsersEntity Users { get; init; }
    
    public int CountryId { get; init; }
}