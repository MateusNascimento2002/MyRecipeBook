using Microsoft.EntityFrameworkCore;

namespace MyRecipeBook.Infrastructure.DataAccess;

public class MyRecipeBookDbContext : DbContext
{
    public MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Domain.Entities.Recipe> Recipes { get; set; }
    public DbSet<Domain.Entities.RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
    }
}
