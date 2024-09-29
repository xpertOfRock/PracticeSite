using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeSite.Data.Identity;
using PracticeSite.Models.Entities;
using PracticeSite.Models.Enums;
using PracticeSite.Models.ValueObjects;

namespace PracticeSite.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {                
                if (!context.Vacancies.Any())
                {
                    var vacancies = new List<Vacancy>
                {
                    new Vacancy
                    {
                        Title = "Далекобійник",
                        Description = "Водій для перевезення вантажів на великі відстані.",
                        Salary = 15000,
                        Status = VacancyStatus.Active,
                        Address = new Address("Київська", "Київ", "Вулиця Транспортна", "01001"),
                        CreatedAt = DateTime.Now
                    },
                    new Vacancy
                    {
                        Title = "Агроном",
                        Description = "Фахівець з вирощування сільськогосподарських культур.",
                        Salary = 18000,
                        Status = VacancyStatus.Active,
                        Address = new Address("Черкаська", "Черкаси", "Аграрна вулиця", "18001"),
                        CreatedAt = DateTime.Now
                    },
                    new Vacancy
                    {
                        Title = "Комбайнер",
                        Description = "Оператор комбайну для збору врожаю.",
                        Salary = 14000,
                        Status = VacancyStatus.Active,
                        Address = new Address("Полтавська", "Полтава", "Тракторна вулиця", "36001"),
                        CreatedAt = DateTime.Now
                    },
                    new Vacancy
                    {
                        Title = "Охоронець",
                        Description = "Відповідає за безпеку сільськогосподарського об'єкту.",
                        Salary = 12000,
                        Status = VacancyStatus.Active,
                        Address = new Address("Харківська", "Харків", "Оборонна вулиця", "61001"),
                        CreatedAt = DateTime.Now
                    },
                    new Vacancy
                    {
                        Title = "Механік",
                        Description = "Ремонт та обслуговування сільськогосподарської техніки.",
                        Salary = 16000,
                        Status = VacancyStatus.Active,
                        Address = new Address("Вінницька", "Вінниця", "Технічна вулиця", "21001"),
                        CreatedAt = DateTime.Now
                    }
                };

                    context.Vacancies.AddRange(vacancies);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

