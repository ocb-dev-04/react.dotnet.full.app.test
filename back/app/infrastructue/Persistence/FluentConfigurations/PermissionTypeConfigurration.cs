using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Value.Objects.Helper.FluentApiConverters.Primitives;

namespace Persistence.FluentConfigurations;

internal sealed class PermissionTypeConfigurration
    : IEntityTypeConfiguration<PermissionType>
{
    public void Configure(EntityTypeBuilder<PermissionType> builder)
    {

        builder.Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(250)
            .HasConversion<StringObjectConverter>();

        builder.Property<byte[]>("version").IsRowVersion();


        builder.HasKey(o => o.Id);
        builder.Metadata.SetSchema("permissions");
    }
}
