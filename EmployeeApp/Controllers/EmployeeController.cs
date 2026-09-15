using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers;

public class EmployeeController : Controller
{
    // In-memory "database" so this sample has no external dependencies.
    // A real app would replace this with EF Core / a real data store.
    private static readonly List<Employee> Employees = new()
    {
        new Employee { Id = 1, Name = "John Smith", Department = "Engineering", Salary = 95000m, HireDate = new DateTime(2019, 3, 1) },
        new Employee { Id = 2, Name = "Jane Doe", Department = "Sales", Salary = 85000m, HireDate = new DateTime(2020, 1, 15) },
    };

    public IActionResult Index()
    {
        return View(Employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        employee.Id = Employees.Count == 0 ? 1 : Employees.Max(e => e.Id) + 1;
        Employees.Add(employee);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var employee = Employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            Employees.Remove(employee);
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error()
    {
        return Problem();
    }
}
