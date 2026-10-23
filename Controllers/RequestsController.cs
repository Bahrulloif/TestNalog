using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestNalog.Data;
using TestNalog.Enums;
using TestNalog.Models;
using TestNalog.ViewModels;

namespace TestNalog.Controllers;

public class RequestsController : Controller
{
    private readonly ApplicationDbContext _context;
    public RequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task FillEmployees(RequestCreateViewModel model)
    {
        model.Employees = await _context
        .Employees.OrderBy(e => e.FullName).
        Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.FullName })
        .ToListAsync();

    }
    public async Task<ActionResult> Index()
    {
        var requests = await _context.Requests.Include(r => r.Employee).ToListAsync();
        return View(requests);
    }

    // GET: Requests/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new RequestCreateViewModel();
        await FillEmployees(model);
        return View(model);
    }

    // POST: Requests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequestCreateViewModel model)
    {
        if (!Enum.IsDefined(typeof(RequestPriority), model.Priority))
        {
            ModelState.AddModelError(
                nameof(model.Priority),
                "Выбран недопустимый приоритет.");
        }
        if (!ModelState.IsValid)
        {
            await FillEmployees(model);
            return View(model);
        }

        var existEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == model.EmployeeId);
        if (existEmployee == null)
        {
            ModelState.AddModelError(
                nameof(model.EmployeeId),
                "Выбранный сотрудник не найден.");
            await FillEmployees(model);
            return View(model);
        }

        var request = new Request
        {
            EmployeeId = model.EmployeeId,
            Title = model.Title,
            Description = model.Description,
            Priority = model.Priority,
            CreatedDate = DateTime.UtcNow,
            Status = Enums.RequestStatus.New

        };
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<ActionResult> Detail(int? id)
    {
        if (id == null || id <= 0)
        {
            return NotFound();
        }
        var request = await _context.Requests
        .Include(e => e.Employee)
        .FirstOrDefaultAsync(r => r.Id == id);
        if (request == null)
        {
            return NotFound();
        }
        else
        {
            return View(request);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id <= 0)
        {
            return NotFound();
        }
        var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        if (request == null)
        {
            return NotFound();
        }
        else
        {
            var model = new RequestCreateViewModel
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                EmployeeId = request.EmployeeId
            };
            return View(model);
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RequestCreateViewModel model)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
        if (request == null)
        {
            return NotFound();
        }
        if (request.Status == RequestStatus.Cancelled || request.Status == RequestStatus.Completed)
        {
            return BadRequest("Эту заявку нельзя изменить");
        }
        if (!Enum.IsDefined(typeof(RequestPriority), model.Priority))
        {
            ModelState.AddModelError(
                nameof(model.Priority),
                "Выбран недопустимый приоритет.");
        }
        var employeeExists = await _context.Employees
        .AnyAsync(e => e.Id == model.EmployeeId);

        if (employeeExists == false)
        {
            ModelState.AddModelError(nameof(model.EmployeeId), "Выбранный сотрудник не найден.");

            await FillEmployees(model);
            return View(model);
        }

        request.Title = model.Title;
        request.Description = model.Description;
        request.EmployeeId = model.EmployeeId;
        request.Priority = model.Priority;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
