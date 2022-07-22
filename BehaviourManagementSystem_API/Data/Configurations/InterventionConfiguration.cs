using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class InterventionConfiguration : IEntityTypeConfiguration<Intervention>
    {
        public void Configure(EntityTypeBuilder<Intervention> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Assesetment)
                .WithMany(prop => prop.Interventions)
                .HasForeignKey(prop => prop.AssesetmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.ProfileMild)
                .WithMany(prop => prop.Interventions)
                .HasForeignKey(prop => prop.ProfileMildId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.ProfileModerate)
                .WithMany(prop => prop.Interventions)
                .HasForeignKey(prop => prop.ProfileModerateId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.ProfileExtreme)
                .WithMany(prop => prop.Interventions)
                .HasForeignKey(prop => prop.ProfileExtremeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.ProfileRecovery)
                .WithMany(prop => prop.Interventions)
                .HasForeignKey(prop => prop.ProfileRecoveryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
