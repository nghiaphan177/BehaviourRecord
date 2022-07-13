using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class IndividualConfiguration : IEntityTypeConfiguration<Individual>
    {
        public void Configure(EntityTypeBuilder<Individual> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.HasOne(prop => prop.User)
                .WithMany(prop => prop.Individuals)
                .HasForeignKey(prop => prop.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
