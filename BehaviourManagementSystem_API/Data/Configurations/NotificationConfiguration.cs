using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.User)
              .WithMany(prop => prop.Notifications)
              .HasForeignKey(prop => prop.UserId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
