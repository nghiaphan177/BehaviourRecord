using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    public class AnalyzeAntecedentEnvironmentalConfiguration : IEntityTypeConfiguration<AnalyzeAntecedentEnvironmental>
    {
        public void Configure(EntityTypeBuilder<AnalyzeAntecedentEnvironmental> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
