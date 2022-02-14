using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace hafta1WebApi.DBOperations
{
    public class DataGenerator
    {
        public static string sifre = "password";
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using ( var context = new TaskDbContext(serviceProvider.GetRequiredService<DbContextOptions<TaskDbContext>>()))
            {
                if (context.Tasks.Any())
                {
                    return;
                }

                context.Tasks.AddRange(
                     new Tasklar
                     {
                         //Id = 1,
                         Title = "Patika Dersleri",
                         Description = ".Net patikasını takip et",
                         Status = "Aktif",
                         Date = new DateTime(2022, 01, 22)


                     },
                     new Tasklar
                     {
                         //Id = 2,
                         Title = "Office Hours ",
                         Description = "Perşembe günü office hours katıp 20:00",
                         Status = "Pasif",
                         Date = new DateTime(2022, 01, 13)

                     },
                     new Tasklar
                     {
                         //Id = 3,
                         Title = "Ödev",
                         Description = "Haft 1 ödevini yap!",
                         Status = "Aktif",
                         Date = new DateTime(2022, 01, 15)
                     },
                     new Tasklar
                     {
                         //Id = 4,
                         Title = "Oyun",
                         Description = "Oyun oyna",
                         Status = "Aktif",
                         Date = new DateTime(2022, 01, 14)
                     }
                        );
                context.SaveChanges();
            }
            
            using (var context2 = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                if (context2.Users.Any())
                {
                    return;
                }


                context2.Users.AddRange(
                    new User
                    {
                        Email = "johndoe@gmail.com",
                        Password = sifre.Encryptor(),
                        Role = "Admin"  

                    },
                     new User
                     {
                         Email = "johnaktas@gmail.com",
                         Password = sifre.Encryptor(),
                         Role = "User",

                     },
                      new User
                      {
                          Email = "johnpatik@gmail.com",
                          Password = sifre.Encryptor(),
                          Role = "User"

                      }

                    );
                context2.SaveChanges();
            }

            }
    }
}
