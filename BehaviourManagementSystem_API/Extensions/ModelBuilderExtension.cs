using BehaviourManagementSystem_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace BehaviourManagementSystem_API.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid();
            var roleAdminId = Guid.NewGuid();
            var stamp = Guid.NewGuid();
            var passwordHasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = userId,
                    FirstName = "Lê",
                    LastName = "Hoàng Duy",
                    Gender = "Nam",
                    DOB = new DateTime(1998, 12, 30),
                    AvtName = "default_avt.png",
                    Activity = true,
                    ActivityDate = new DateTime(9999, 12, 31),
                    UserName = "admin",
                    NormalizedUserName = "admin".ToUpper(),
                    PasswordHash = passwordHasher.HashPassword(null, "Password2@"),
                    Email = "lhduy12cb34@gmail.com",
                    NormalizedEmail = "lhduy12cb34@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "0334102197",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = stamp.ToString(),
                    ConcurrencyStamp = stamp.ToString().ToUpper(),
                });

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = roleAdminId,
                    Name = "admin",
                    NormalizedName = "admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "teacher",
                    NormalizedName = "teacher".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "student",
                    NormalizedName = "student".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString().ToUpper(),
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>()
                {
                    UserId = userId,
                    RoleId = roleAdminId,
                });
        }
    }
}
