using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database.TypeConfigurations; 

public class SmeuTypeConfiguration: IEntityTypeConfiguration<Smeu> {
    public void Configure(EntityTypeBuilder<Smeu> builder) {

        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Value).IsRequired().IsUnicode();
        builder.HasOne(row => row.Author)
            .WithMany().HasForeignKey(prop => prop.AuthorId);
        builder.Property(row => row.DiscordId).IsRequired();
        builder.Property(prop => prop.SubmittedOn).IsRequired();
        builder.Property(prop => prop.ProcessedOn).IsRequired();
        builder.Property(prop => prop.Version).HasDefaultValue(0).IsRowVersion();
        
        builder.HasIndex(prop => prop.Value).IsUnique();
        builder.HasIndex(prop => prop.DiscordId).IsUnique();
        builder.HasIndex(prop => prop.AuthorId);
    }
}