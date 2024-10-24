using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Exercises.Domain.Clients;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Clients;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(user => user.TableId);

        builder.Property(user => user.TableId).UseIdentityColumn();

        builder.Property(user => user.Id).IsRequired();

        builder.HasIndex(model => model.Id)
            .IsUnique();

        builder.Property(c => c.FirstName).HasMaxLength(200);

        builder.Property(c => c.LastName).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(300);
    }
}
