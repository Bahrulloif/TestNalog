using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestNalog.Data;
using TestNalog.Models;
using TestNalog.ViewModels;

namespace TestNalog.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View(new EmployeeFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var employee = new Employee
            {
                FullName = model.FullName,
                Department = model.Department,
                Phone = model.Phone
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View(employee);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EmployeeFormViewModel
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Department = employee.Department,
                    Phone = employee.Phone
                };
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeFormViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.FullName = model.FullName;
                employee.Department = model.Department;
                employee.Phone = model.Phone;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View(employee);
            }
        }



        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existingRequest = await _context.Requests.FirstOrDefaultAsync(r => r.EmployeeId == id);
            var existingEmployee = await _context.Employees.FindAsync(id);

            if (existingEmployee != null && existingRequest == null)
            {
                _context.Employees.Remove(existingEmployee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
