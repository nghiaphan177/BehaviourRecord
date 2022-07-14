using BehaviourManagementSystem_API.Data.Configurations;
using BehaviourManagementSystem_API.Extensions;
using BehaviourManagementSystem_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BehaviourManagementSystem_API.Data.EF
{
    /// <summary>
    /// Create Db Context
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<AnalyzeAntecedentActivity> AnalyzeAntecedentActivities { get; set; }
        public virtual DbSet<AnalyzeAntecedentEnvironmental> AnalyzeAntecedentEnvironmentals { get; set; }
        public virtual DbSet<AnalyzeAntecedentPerceive> AnalyzeAntecedentPerceives { get; set; }
        public virtual DbSet<Assesetment> Assesetments { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<ProfileExtreme> ProfileExtremes { get; set; }
        public virtual DbSet<ProfileMild> ProfileMilds { get; set; }
        public virtual DbSet<ProfileModerate> ProfileModerates { get; set; }
        public virtual DbSet<ProfileRecovery> ProfileRecoveries { get; set; }
        public virtual DbSet<TermCondition> TermConditions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Remove AspNet prefix of tables: default tables in IdentityDbContext
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Configure using Fluent API. 
            builder.ApplyConfiguration(new AnalyzeAntecedentActivityConfiguration());
            builder.ApplyConfiguration(new AnalyzeAntecedentEnvironmentalConfiguration());
            builder.ApplyConfiguration(new AnalyzeAntecedentPerceiveConfiguration());
            builder.ApplyConfiguration(new AssesetmentConfiguration());
            builder.ApplyConfiguration(new IndividualConfiguration());
            builder.ApplyConfiguration(new InterventionConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new ProfileExtremeConfiguration());
            builder.ApplyConfiguration(new ProfileMildConfiguration());
            builder.ApplyConfiguration(new ProfileModerateConfiguration());
            builder.ApplyConfiguration(new ProfileRecoveryConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new TermConditionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            // Data seed
            builder.Seed();
        }
    }
}
