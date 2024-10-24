using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;

public class ParqResponseConfiguration : IEntityTypeConfiguration<ParqResponse>
{
    public void Configure(EntityTypeBuilder<ParqResponse> builder)
    {
        builder.ToTable("parq_responses");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.QuestionId).IsRequired();
        
        builder.Property(x => x.Response).IsRequired()
            .HasMaxLength(250);
        
        builder.Property(x => x.ResponseDate).IsRequired();
        
        builder.HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId)
            .IsRequired();
    }
}
