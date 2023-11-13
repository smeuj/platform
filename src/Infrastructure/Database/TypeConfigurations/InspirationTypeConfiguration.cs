using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database.TypeConfigurations; 

public class InspirationTypeConfiguration: IEntityTypeConfiguration<Inspiration> {
    
    public void Configure(EntityTypeBuilder<Inspiration> builder) {
        
        builder.HasKey(ins => ins.Id);
        builder.Property(ins => ins.Type).IsRequired();
        builder.Property(ins => ins.SubmittedOn).IsRequired();
        builder.Property(ins => ins.ProcessedOn).IsRequired();
        builder.Property(ins => ins.Version).HasDefaultValue(0).IsRowVersion();
        builder.Property(ins => ins.Value).IsUnicode();

        builder.HasOne(row => row.Author).WithMany()
            .HasForeignKey(author => author.AuthorId);

        builder.HasIndex(ins => ins.SmeuId);
    }
}