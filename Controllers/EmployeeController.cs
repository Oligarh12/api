using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Employee;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using api.Models;

namespace api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly UserManager<AppUser> _userManager;
        public EmployeeController(DBContext dbContext, IEmployeeRepository employeeRepo, UserManager<AppUser> userManager)
        {
            _employeeRepo = employeeRepo;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private async Task<long?> GetUserCompanyId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.CompanyId;
        }

        // GET: api/employee
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var employees = await _employeeRepo.GetAllByCompanyAsync(companyId.Value);
            var dto = employees.Select(e => e.ToEmployeeDTO());
            return Ok(dto);
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeById([FromRoute] long id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var companyId = await GetUserCompanyId();
            if (companyId == null || employee.CompanyId != companyId) return Forbid();
            return Ok(employee.ToEmployeeDTO());
        }

        // POST: api/employee
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var companyId = await GetUserCompanyId();
            if (companyId == null || companyId != dto.CompanyId) return Forbid();
            var model = dto.ToEmployeeCreate();
            await _employeeRepo.CreateAsync(model);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = model.Id }, model.ToEmployeeDTO());
        }

        // PUT: api/employee/{id}
        [HttpPut("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateEmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var updated = dto.ToEmployeeUpdate();
            var result = await _employeeRepo.UpdateAsync(id, updated);
            if (result == null)
            {
                return NotFound();
            }
            if (result.CompanyId != companyId) return Forbid();
            return Ok(result.ToEmployeeDTO());
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var emp = await _employeeRepo.GetByIdAsync(id);
            if (emp == null || emp.CompanyId != companyId) return NotFound();
            var ok = await _employeeRepo.DeleteAsync(id);
            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
