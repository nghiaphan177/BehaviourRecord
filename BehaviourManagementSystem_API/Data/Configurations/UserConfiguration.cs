using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
