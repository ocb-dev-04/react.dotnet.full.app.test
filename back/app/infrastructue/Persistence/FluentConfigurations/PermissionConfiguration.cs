using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Value.Objects.Helper.FluentApiConverters.Primitives;

namespace Persistence.FluentConfigurations;

internal sealed class PermissionConfiguration
    : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        builder.Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.EmployeeName)
            .IsRequired()
            .HasMaxLength(150)
            .HasConversion<StringObjectConverter>();

        builder.Property(e => e.EmployeeLastName)
            .IsRequired()
            .HasMaxLength(150)
            .HasConversion<StringObjectConverter>();
        
        builder.Property(p => p.PermissionTypeId).IsRequired();

        builder.HasOne(p => p.PermissionType)
            .WithMany()
            .HasForeignKey(p => p.PermissionTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<byte[]>("version").IsRowVersion();


        builder.HasKey(o => o.Id);
        builder.HasIndex(i =>
            new
            {
                i.EmployeeName,
                i.EmployeeLastName,
                i.PermissionTypeId
            });

        builder.Metadata.SetSchema("permissions");
    }
}
