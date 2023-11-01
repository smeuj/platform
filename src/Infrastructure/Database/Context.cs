using Microsoft.EntityFrameworkCore;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database; 

public class Context:DbContext {

    private readonly string? connectionString;
    
    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Smeu> Smeuj => Set<Smeu>();
    
    public Context() {
    }

    public Context(string connectionString) {
        this.connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(connectionString ?? "Data Source=SmeujPlatform.db");
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}