using GloboClimaPlatform.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboClimaPlatform.Infra;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UsersEntity> UsersEntities { get; init; }
}