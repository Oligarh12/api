using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Employee;
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

        // GET: api/employee
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepo.GetAllAsync();
            var dto = employees.Select(e => e.ToEmployeeDTO());
            return Ok(dto);
        }

        // GET: api/employee/{id}
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

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = dto.ToEmployeeCreate();
            await _employeeRepo.CreateAsync(model);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = model.Id }, model.ToEmployeeDTO());
        }

        // PUT: api/employee/{id}
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateEmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = dto.ToEmployeeUpdate();
            var result = await _employeeRepo.UpdateAsync(id, updated);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.ToEmployeeDTO());
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var ok = await _employeeRepo.DeleteAsync(id);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
