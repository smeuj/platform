using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database.TypeConfigurations; 

public class SmeujEntryTypeConfiguration: IEntityTypeConfiguration<SmeujEntry> {
    public void Configure(EntityTypeBuilder<SmeujEntry> builder) {

        builder.HasKey(prop => prop.Id);
        
        builder.Property(prop => prop.Smeuj).IsRequired().IsUnicode();
        builder.HasOne(row => row.Author)
            .WithMany().HasForeignKey(prop => prop.AuthorId);
        builder.Property(row => row.DiscordId).IsRequired();
        builder.Property(prop => prop.SubmittedOn).IsRequired();
        builder.Property(prop => prop.ProcessedOn).IsRequired();
        builder.Property(prop => prop.Version).HasDefaultValue(0).IsRowVersion();
        
        builder.HasIndex(prop => prop.Smeuj).IsUnique();
        builder.HasIndex(prop => prop.DiscordId).IsUnique();
        builder.HasIndex(prop => prop.AuthorId);
    }
}