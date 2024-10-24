using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;

public class ParqCompletionConfiguration : IEntityTypeConfiguration<ParQCompletion>
{
    public void Configure(EntityTypeBuilder<ParQCompletion> builder)
    {
        builder.ToTable("parq_completions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        
        builder.Property(e => e.CompletionDate)
            .IsRequired();
        
        builder.Property(e => e.PdfUrl)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasOne<User>()
            .WithMany()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(n => n.UserId);
    }
}
