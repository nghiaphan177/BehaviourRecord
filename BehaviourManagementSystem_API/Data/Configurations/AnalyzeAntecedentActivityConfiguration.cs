using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class AnalyzeAntecedentActivityConfiguration : IEntityTypeConfiguration<AnalyzeAntecedentActivity>
    {
        public void Configure(EntityTypeBuilder<AnalyzeAntecedentActivity> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
