using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeSite.Data.Identity;
using PracticeSite.Data;
using PracticeSite.Models.Enums;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class VacancyController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public VacancyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var vacancies = await _context.Vacancies.Where(v => v.Status == VacancyStatus.Active).ToListAsync();
        return View(vacancies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        return View(vacancy);
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Apply(int id)
    {
        var vacancy = await _context.Vacancies
                                    .Include(v => v.RespondedUsers)
                                    .FirstOrDefaultAsync(v => v.Id == id);

        if (vacancy == null || vacancy.Status == VacancyStatus.Closed)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);

        if (vacancy.RespondedUsers.Any(u => u.Id == user.Id))
        {
            TempData["ErrorMessage"] = "Ви вже подали заявку на цю вакансію.";
            return RedirectToAction(nameof(Index), new { id });
        }

        vacancy.RespondedUsers.Add(user);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
