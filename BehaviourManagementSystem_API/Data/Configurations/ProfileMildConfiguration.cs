using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    public class ProfileMildConfiguration : IEntityTypeConfiguration<ProfileMild>
    {
        public void Configure(EntityTypeBuilder<ProfileMild> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
