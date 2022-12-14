using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    public class ProfileModerateConfiguration : IEntityTypeConfiguration<ProfileModerate>
    {
        public void Configure(EntityTypeBuilder<ProfileModerate> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
