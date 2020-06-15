using System;
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

            //проверка наличия групп объектов
            if (!context.FlowerGroups.Any())
            {
                context.FlowerGroups.AddRange(
                new List<FlowerGroup>
                {
                new FlowerGroup {GroupName="Амариллисовые"},
                new FlowerGroup {GroupName="Астровые"},
                new FlowerGroup {GroupName="Ирисовые"},
                new FlowerGroup {GroupName="Лилейные"},
                new FlowerGroup {GroupName="Лютиковые"},
                new FlowerGroup {GroupName="Пионовые"},
                new FlowerGroup {GroupName="Орхидные"},
                new FlowerGroup {GroupName="Яснотковые"}
                });
                await context.SaveChangesAsync();
            }
            // проверка наличия объектов
            if (!context.Flowers.Any())
            {
                context.Flowers.AddRange(
                new List<Flower>
                {   
                new Flower { FlowerName = "Амариллис", FlowerDescription = "Цветущее луковичное растение. Место происхождения: Южная Африка", FlowerPrice = 35, FlowerGroupId = 1, FlowerImage = "amaryllis.jpg" },
                new Flower { FlowerName = "Анемон", FlowerDescription = "маковидные и ромашковидные цветы. Место происхождения: Южная Европа, Центральная Азия", FlowerPrice = 18, FlowerGroupId = 5, FlowerImage = "anemone.jpg" },
                new Flower { FlowerName = "Крокус (Шафран)", FlowerDescription = "Место происхождения: Индия", FlowerPrice = 25, FlowerGroupId = 3, FlowerImage = "Crocus.jpg" },
                new Flower { FlowerName = "Лаванда", FlowerDescription = "Место происхождения: Средиземноморье", FlowerPrice = 18, FlowerGroupId = 8, FlowerImage = "Lavandula.jpg" },
                new Flower { FlowerName = "Нарцисс", FlowerDescription = "Место происхождения: Центральная и Западная Европа", FlowerPrice = 19, FlowerGroupId = 1, FlowerImage = "narcissus.jpg" },
                new Flower { FlowerName = "Орхидея Цимбидиум", FlowerDescription = "Место происхождения: Юго-Восточная Азия", FlowerPrice = 38, FlowerGroupId = 7, FlowerImage = "Cymbidium.jpg" },
                new Flower { FlowerName = "Пион", FlowerDescription = "Место происхождения: Сибирь, Западный Китай, Монголия", FlowerPrice = 34, FlowerGroupId = 6, FlowerImage = "Paeonia.jpg" },
                new Flower { FlowerName = "Тюльпан", FlowerDescription = "Многолетние луковичные растения. Место происхождения: Казахстан", FlowerPrice = 12, FlowerGroupId = 4, FlowerImage = "Tulipa.jpg" },
                new Flower {FlowerName = "Хризантема кустовая", FlowerDescription = "Место происхождения: Азия", FlowerPrice = 15, FlowerGroupId = 2, FlowerImage = "Chrysanthemum.jpg" }

                });
                await context.SaveChangesAsync();
            }
        }
    }
 }
