using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeuj.Platform.Domain;

namespace Smeuj.Platform.Infrastructure.Database.TypeConfigurations; 

public class ExampleTypeConfiguration: IEntityTypeConfiguration<Example>  {
    public void Configure(EntityTypeBuilder<Example> builder) {

        builder.HasKey(example => example.Id);

        builder.Property(example => example.Value).IsRequired().IsUnicode();
        builder.Property(example => example.SubmittedOn).IsRequired();
        builder.Property(example => example.ProcessedOn).IsRequired();
        builder.Property(example => example.Version).HasDefaultValue(0).IsRowVersion();
        
        builder.HasOne<Author>(row => row.Author).WithMany()
            .HasForeignKey(row => row.AuthorId);

        builder.HasIndex(ins => ins.SmeuId);
    }
}