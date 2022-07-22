using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class ProfileExtremeConfiguration : IEntityTypeConfiguration<ProfileExtreme>
    {
        public void Configure(EntityTypeBuilder<ProfileExtreme> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
