using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagpayApi.Models;

namespace DagpayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AzureDatabaseContext _context;

        public EmployeeController(AzureDatabaseContext context)
        {
            _context = context;

            if (_context.Employees.Count() == 0)
            {
                _context.Employees.Add(new Employee { 
                    FirstName = "Derick",
                    LastName = "Gross",
                    Department = "Development",
                    EmployeeId = 1,
                    Experience = 5
                });
                _context.SaveChanges();
            }
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            //return await _context.Employees.ToListAsync();
            return  await _context
                            .Employees
                            .Include("Dependents")
                            .ToListAsync();                            
        }

        //GET: api/Employee/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Dependents = _context.Dependents.Where(d => d.EmployeeId == id).ToList();

            Console.WriteLine(employee);

            return employee;
        }

        //POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }
    }
}
