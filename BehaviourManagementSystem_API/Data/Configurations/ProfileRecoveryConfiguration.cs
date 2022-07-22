using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class ProfileRecoveryConfiguration : IEntityTypeConfiguration<ProfileRecovery>
    {
        public void Configure(EntityTypeBuilder<ProfileRecovery> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
