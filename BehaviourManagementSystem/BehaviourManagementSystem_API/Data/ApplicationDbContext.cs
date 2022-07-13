using BehaviourManagementSystem_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class ApplicationDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed> logger)
        {
            var stamp = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid();
            var roleAdminId = Guid.NewGuid();
            if(!await context.Users.AnyAsync())
            {
                var user = new User()
                {
                    Id = userId,
                    UserName = "admin",
                    NormalizedUserName = "admin".ToUpper(),
                    Email = "lhduy3011@gmail.com",
                    NormalizedEmail = "lhduy3011@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "0334102197",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = stamp,
                    ConcurrencyStamp = stamp.ToUpper(),
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Password2@");
                await context.Users.AddAsync(user);
            }

            if(!await context.Roles.AnyAsync())
            {
                await context.Roles.AddAsync(new Role()
                {
                    Id = roleAdminId,
                    Name = "admin",
                    NormalizedName = "admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                });

                await context.Roles.AddAsync(new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "teacher",
                    NormalizedName = "teacher".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                });

                await context.Roles.AddAsync(new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "student",
                    NormalizedName = "student".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                });
            }

            if(!await context.UserRoles.AnyAsync())
            {
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    UserId = userId,
                    RoleId = roleAdminId,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
