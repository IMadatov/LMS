using Auth.Domain.Entities;
using General.Enums;
using General.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Context;

public static class AuthContextSeed
{
    private static List<ApplicationRole> applicationRoles = new List<ApplicationRole>
    {
        new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = Roles.ADMIN.ToString(),
                NormalizedName = Roles.ADMIN.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = Roles.USER.ToString(),
                NormalizedName = Roles.USER.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = Roles.STUDENT.ToString(),
                NormalizedName = Roles.STUDENT.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = Roles.TEACHER.ToString(),
                NormalizedName = Roles.TEACHER.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
    };

    private static List<ApplicationUser> applicationUsers = new List<ApplicationUser>
    {
        new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            NormalizedUserName = "admin".ToUpper(),
            MainRole = Roles.ADMIN.ToString(),
            FirstName = "AdminFirstName",
            LastName = "AdminLastName",
            Language = Languages.ENGLISH,
            SecurityStamp=string.Empty,
        },
        new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "user",
            NormalizedUserName = "user".ToUpper(),
            MainRole = Roles.USER.ToString(),
            FirstName = "UserFirstName",
            LastName = "UserLastName",
            Language = Languages.ENGLISH,
            SecurityStamp=string.Empty,
        },
        new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "student",
            NormalizedUserName = "student".ToUpper(),
            MainRole = Roles.STUDENT.ToString(),
            FirstName = "StudentFirstName",
            LastName = "StudentLastName",
            Language = Languages.ENGLISH,
            SecurityStamp=string.Empty,
        },
        new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "teacher",
            NormalizedUserName = "teacher".ToUpper(),
            MainRole = Roles.TEACHER.ToString(),
            FirstName = "TeacherFirstName",
            LastName = "TeacherLastName",
            Language = Languages.ENGLISH,
            SecurityStamp=string.Empty,
        }
    };


    public static void Seed(this ModelBuilder builder)
    {
        builder.SeedRoles();
        builder.SeedUsers();
    }

    private static void SeedRoles(this ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>(roles =>
        {
            roles.HasData(applicationRoles);
        });
    }

    private static void SeedUsers(this ModelBuilder builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.Entity<ApplicationUser>(users =>
        {
            
            applicationUsers.ForEach(us =>
            {
                us.PasswordHash = hasher.HashPassword(us, "Qwerty123$");
            });

            users.HasData(applicationUsers);



            var IdentityUserRoleList = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid>
                {
                    UserId=applicationUsers[0].Id,
                    RoleId=applicationRoles[0].Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId=applicationUsers[1].Id,
                    RoleId=applicationRoles[1].Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId=applicationUsers[2].Id,
                    RoleId=applicationRoles[2].Id
                },
                new IdentityUserRole<Guid>
                {
                    UserId=applicationUsers[3].Id,
                    RoleId=applicationRoles[3].Id
                }
            };

            builder.Entity<IdentityUserRole<Guid>>()
            .HasData(IdentityUserRoleList);
        });
    }
}
