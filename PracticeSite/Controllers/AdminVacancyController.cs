using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeSite.Data;
using PracticeSite.Models.Entities;
using PracticeSite.Models.Enums;
using PracticeSite.Models.ValueObjects;
using PracticeSite.Models.ViewModels;

[Authorize(Roles = "admin")]
public class AdminVacancyController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminVacancyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchTitle, string sortOrder)
    {
        var vacancies = _context.Vacancies.AsQueryable();

        if (!string.IsNullOrEmpty(searchTitle))
        {
            vacancies = vacancies.Where(v => v.Title.Contains(searchTitle));
        }

        vacancies = sortOrder switch
        {
            "salary" => vacancies.OrderBy(v => v.Salary),
            "status" => vacancies.OrderBy(v => v.Status),
            _ => vacancies.OrderBy(v => v.Title),
        };

        var result = await vacancies.ToListAsync();
        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new VacancyViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(VacancyViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var vacancy = new Vacancy
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Salary = viewModel.Salary,
            Address = new Address(viewModel.State, viewModel.City, viewModel.Street, viewModel.PostalCode),
            Status = VacancyStatus.Active
        };

        _context.Vacancies.Add(vacancy);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy == null)
        {
            return NotFound();
        }

        var viewModel = new VacancyViewModel
        {
            Title = vacancy.Title,
            Description = vacancy.Description,
            Salary = vacancy.Salary,
            State = vacancy.Address.State,
            City = vacancy.Address.City,
            Street = vacancy.Address.Street,
            PostalCode = vacancy.Address.PostalCode,
            Status = vacancy.Status
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, VacancyViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy == null)
        {
            return NotFound();
        }

        vacancy.Title = viewModel.Title;
        vacancy.Description = viewModel.Description;
        vacancy.Salary = viewModel.Salary;
        vacancy.Address = new Address(viewModel.State, viewModel.City, viewModel.Street, viewModel.PostalCode);
        vacancy.Status = viewModel.Status;

        _context.Vacancies.Update(vacancy);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Close(int id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy == null)
        {
            return NotFound();
        }

        vacancy.Status = VacancyStatus.Closed;
        _context.Vacancies.Update(vacancy);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var vacancy = await _context.Vacancies.Include(v => v.RespondedUsers).FirstOrDefaultAsync(v => v.Id == id);
        if (vacancy == null)
        {
            return NotFound();
        }

        return View(vacancy);
    }
}
