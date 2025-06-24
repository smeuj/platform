using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database.TypeConfigurations; 

public class AuthorTypeConfiguration:IEntityTypeConfiguration<Author> {
    
    public void Configure(EntityTypeBuilder<Author> builder) {

        builder.HasKey(row => row.Id);

        builder.Property(prop => prop.PublicName).IsRequired().HasMaxLength(200);
        builder.Property(prop => prop.Name).IsRequired().HasMaxLength(400);
        builder.Property(prop => prop.DiscordId);
        builder.Property(prop => prop.AuthorSince).IsRequired();
        builder.Property(prop => prop.Version).HasDefaultValue(0).IsRowVersion();
        
        builder.HasIndex(prop => prop.DiscordId).IsUnique();
    }
}