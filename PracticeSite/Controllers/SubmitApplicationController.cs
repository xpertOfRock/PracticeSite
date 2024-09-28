using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeSite.Data;
using PracticeSite.Models.Entities;
using PracticeSite.Models.Enums;
using PracticeSite.Models.ValueObjects;
using PracticeSite.Models.ViewModels;

namespace PracticeSite.Controllers
{
    public class SubmitApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmitApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Показать форму подачи заявки
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        // POST: Обработка подачи заявки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ApplicationFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var applicationForm = new ApplicationForm
                {
                    Title = model.Title,
                    Description = model.Description,
                    FullName = model.FullName,
                    Age = model.Age,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    LandArea = model.LandArea,
                    PricePerHectare = model.PricePerHectare,
                    Status = ApplicationStatus.InProgress,
                    Address = new Address(model.State, model.City, model.Street, model.PostalCode)
                };

                _context.Applications.Add(applicationForm);
                await _context.SaveChangesAsync();

                // Вывод сообщения об успешной подаче
                TempData["SuccessMessage"] = "Ваша заявка успешно отправлена!";
                return RedirectToAction("Submit");
            }

            return View(model);
        }
    }
}