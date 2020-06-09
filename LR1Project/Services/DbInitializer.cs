﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab.DAL.Entities;
using WebLab.DAL.Data;
using Microsoft.AspNetCore.Identity;

namespace LR1Project
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        // создать БД, если она еще не создана
        { context.Database.EnsureCreated();
        // проверка наличия ролей
        if(!context.Roles.Any())
        {
        var roleAdmin = new IdentityRole
        {
            Name = "admin",
            NormalizedName = "admin"
        };
        // создать роль manager
        var result = await roleManager.CreateAsync(roleAdmin);
    }
        // проверка наличия пользователей
        if(!context.Users.Any())
        {
        // создать пользователя user@mail.ru
        var user = new ApplicationUser
        {
            Email = "user@mail.ru",
            UserName = "user@mail.ru"
        };
    await userManager.CreateAsync(user, "123456");
    // создать пользователя admin@mail.ru
    var admin = new ApplicationUser
    {
        Email = "admin@mail.ru",
        UserName = "admin@mail.ru"
    };
    await userManager.CreateAsync(admin, "123456");
    // назначить роль admin
    admin = await userManager.FindByEmailAsync("admin@mail.ru");
    await userManager.AddToRoleAsync(admin, "admin");
}
        }
    }
 }
