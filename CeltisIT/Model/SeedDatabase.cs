using CeltisIT.Model.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeltisIT.Model
{
    public static class SeedDatabase
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CeltisITContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<CeltisITContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(new User
                {
                    FullName = "Emiola Abdulmueez",
                    Password = "Password1234",
                    UserName = "Abdul",
                    UserRole = "Admin"
                },
                new User
                {
                    FullName = "Olalemi Williams",
                    Password = "Password1234",
                    UserName = "Willy",
                    UserRole = "Admin"
                }
                );
            }
            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item
                    {
                        Title = "Web Server",
                        Description = "IIS 7.0",
                        UnitType = "Hours",
                        Rate = 20
                    },
                    new Item
                    {
                        Title = "Logo Design",
                        Description = "Designed a logo for app",
                        UnitType = "PC",
                        Rate = 100
                    },
                    new Item
                    {
                        Title = "Application Development",
                        Description = "PHP application for project management",
                        UnitType = "Hours",
                        Rate = 20
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}
