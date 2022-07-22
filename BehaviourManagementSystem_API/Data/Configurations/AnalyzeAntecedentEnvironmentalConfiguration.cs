using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class AnalyzeAntecedentEnvironmentalConfiguration : IEntityTypeConfiguration<AnalyzeAntecedentEnvironmental>
    {
        public void Configure(EntityTypeBuilder<AnalyzeAntecedentEnvironmental> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
