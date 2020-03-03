using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations
{
    public class Profile_EventConfiguration : IEntityTypeConfiguration<Profile_Event>
    {
        public void Configure(EntityTypeBuilder<Profile_Event> builder)
        {
            builder.ToTable("Profiles_Events");

            builder.HasKey(pe => new { pe.ProfileId, pe.EventId });

            builder.Property(pe => pe.ProfileId).HasColumnName("ProfileId");
            builder.Property(pe => pe.EventId).HasColumnName("EventId");

            builder.HasOne(pe => pe.Profile).WithMany(p => p.Events).HasForeignKey(pe => pe.ProfileId);
            builder.HasOne(pe => pe.Event).WithMany(e => e.Profiles).HasForeignKey(pe => pe.EventId);

        }
    }
}
