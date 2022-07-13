using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class AssesetmentConfiguration : IEntityTypeConfiguration<Assesetment>
    {
        public void Configure(EntityTypeBuilder<Assesetment> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.Individual)
                .WithMany(prop => prop.Assesetments)
                .HasForeignKey(prop => prop.IndividualId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.AnalyzeAntecedentActivity)
                .WithMany(prop => prop.Assesetments)
                .HasForeignKey(prop => prop.AnalyzeAntecedentActivityId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.AnalyzeAntecedentEnvironmental)
                .WithMany(prop => prop.Assesetments)
                .HasForeignKey(prop => prop.AnalyzeAntecedentEnvironmentalId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(prop => prop.AnalyzeAntecedentPerceive)
                .WithMany(prop => prop.Assesetments)
                .HasForeignKey(prop => prop.AnalyzeAntecedentPerceivedId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
