using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Some.Root.DefaultNamespace.Data;
public class SomeRecordConfiguration : IEntityTypeConfiguration<SomeRecord>
{
    public void Configure(EntityTypeBuilder<SomeRecord> builder)
    {
        builder.ToTable("some");

        builder.HasKey(x => x.Id);
    }
}
