using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(DBContext dbContext, IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
            _dbContext = dbContext;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] long id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.ToEmployeeDTO());
        }
    }
}