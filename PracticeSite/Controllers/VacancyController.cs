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
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy == null || vacancy.Status == VacancyStatus.Closed)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user.AppliedVacancies.Any(v => v.Id == id))
        {
            ModelState.AddModelError("", "Ви вже подали заявку на цю вакансію.");
            return RedirectToAction(nameof(Details), new { id });
        }

        user.AppliedVacancies.Add(vacancy);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
