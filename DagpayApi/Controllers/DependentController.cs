using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DagpayApi.Models;
using DagpayApi.Helpers;

namespace DagpayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentController : ControllerBase
    {
        private readonly AzureDatabaseContext _context;

        public DependentController(AzureDatabaseContext context)
        {
            _context = context;

            if (_context.Dependents.Count() == 0)
            {
                _context.Dependents.Add(new Dependent
                {
                    DependentId = 1,
                    EmployeeId = 1,
                    FirstName = "DeAnna",
                    LastName = "Gross",
                    Cost = 500,
                    DiscountFactor = DeductionHelpers.CalculateDiscountFactor("DeAnna")
                });
                _context.SaveChanges();
            }
        }

        // GET: api/Dependent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent>>> GetDependents()
        {
            return await _context
                            .Dependents
                            //.Include("Employee")
                            .ToListAsync();
        }

        //GET: api/Dependent/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Dependent>> GetDependent(int id)
        {
            Dependent dependent = await _context.Dependents.FindAsync(id);

            if (dependent == null)
            {
                return NotFound();
            }

            return dependent;
        }

        //POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Dependent>> CreateEmployee(Dependent dependent)
        {
            dependent.Cost = 500;
            dependent.DiscountFactor = DeductionHelpers.CalculateDiscountFactor(employee.FirstName);
            _context.Dependents.Add(dependent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDependent), new { id = dependent.DependentId }, dependent);
        }
    }
}
