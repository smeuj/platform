using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database;

public class Database : DbContext, IDataProtectionKeyContext {
    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Smeu> Smeuj => Set<Smeu>();

    public DbSet<Inspiration> Inspirations => Set<Inspiration>();

    public DbSet<Example> Examples => Set<Example>();

    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

    private readonly DatabaseOptions options = new();

    //Design time constructor
    public Database() : this("Data Source=SmeujPlatform.db") {
    }

    public Database(IConfiguration configuration) {
        configuration.GetSection(DatabaseOptions.DataProtection).Bind(options);
    }

    public Database(string connectionString) {
        options.ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(options.ConnectionString);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning);

#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}