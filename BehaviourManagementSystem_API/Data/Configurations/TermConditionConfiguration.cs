using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    public class TermConditionConfiguration : IEntityTypeConfiguration<TermCondition>
    {
        public void Configure(EntityTypeBuilder<TermCondition> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
