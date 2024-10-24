using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.TableId);

        builder.Property(user => user.TableId).UseIdentityColumn();

        builder.Property(user => user.Id).IsRequired();

        builder.HasIndex(model => model.Id)
            .IsUnique();

        builder.Property(u => u.FirstName).HasMaxLength(200);

        builder.Property(u => u.LastName).HasMaxLength(200);

        builder.Property(u => u.Email).HasMaxLength(300);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasIndex(u => u.IdentityId).IsUnique();
        
        builder.OwnsOne(user => user.FitnessProfile, fitnessProfile =>
        {
            fitnessProfile.ToTable("user_fitness_profiles");
            
            fitnessProfile.Property(fp => fp.FitnessGoals)
                .HasMaxLength(2000);

            fitnessProfile.Property(fp => fp.HealthInformation)
                .HasMaxLength(2000);

            fitnessProfile.Property(fp => fp.DietaryPreferences)
                .HasMaxLength(2000);
        });
        
        builder.OwnsOne(user => user.ParQ, parQ =>
        {
            parQ.ToTable("user_parQs");
            
            parQ.Property(p => p.DateCompleted)
                .HasDefaultValueSql("current_timestamp at time zone 'utc'");
        });
    }
}
