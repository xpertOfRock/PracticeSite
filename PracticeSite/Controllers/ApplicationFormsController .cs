using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeSite.Data.Identity;
using PracticeSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PracticeSite.Models.Enums;
using PracticeSite.ExternalServices.MailKit;

namespace PracticeSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ApplicationFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ApplicationFormsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var applocation = await _context.Applications.FindAsync(id);
            return View(applocation);
        }
        public async Task<IActionResult> Index(string searchName, string searchPhone, string searchAddress)
        {
            var query = _context.Applications.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                query = query.Where(a => a.FullName.Contains(searchName));
            }

            if (!string.IsNullOrWhiteSpace(searchPhone))
            {
                query = query.Where(a => a.PhoneNumber.Contains(searchPhone));
            }

            if (!string.IsNullOrWhiteSpace(searchAddress))
            {
                query = query.Where(a => 

                a.Address.State.Contains(searchAddress) ||
                a.Address.City.Contains(searchAddress) ||
                a.Address.Street.Contains(searchAddress) ||
                a.Address.PostalCode.Contains(searchAddress));
            }

            var applications = await query.ToListAsync();
            return View(applications);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = ApplicationStatus.Accepted;
            await _context.SaveChangesAsync();

            _emailService.SendEmail
            (
                application.Email,
                "Відповідь на заявку",
                $"Вітаємо! Ваша заява (No. {application.Id}) була прийнята!" +
                " Ми прийняли вашу заяву." +
                " Вам на почту прийде лист від адміністратора з подальшими інструкціями"
            );
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = ApplicationStatus.Rejected;
            await _context.SaveChangesAsync();

            _emailService.SendEmail
            (
                application.Email,
                "Відповідь на заявку",
                "На жаль, ваша заява була відхилина." +
                " Так чи інакше ми сподіваємося на подальше співпрацювання, та чекаємо нових пропозицій від Вас!" +
                " Гарного дня!"
            );
            return RedirectToAction(nameof(Index));
        }
    }
}
