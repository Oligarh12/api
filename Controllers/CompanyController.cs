using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Company;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public CompanyController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _dbContext.Companies.ToListAsync();
            var companyDto = companies.Select(s => s.ToCompanyDTO());
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] int id)
        {
            var company = await _dbContext.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company.ToCompanyDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequestDto companyDto)
        {
            var companyModel = companyDto.ToCompanyCreate();

            await _dbContext.Companies.AddAsync(companyModel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompanyById), new { id = companyModel.Id }, companyModel.ToCompanyDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCompanyRequestDto updateDto)
        {
            var companyModel = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyModel == null)
            {
                return NotFound();
            }

            companyModel.CompanyName = updateDto.CompanyName;
            companyModel.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(companyModel.ToCompanyDTO());
        }

        /*
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var companyModel = _dbContext.Companies.FirstOrDefault(x => x.Id == id);

            if (companyModel == null)
            {
                return NotFound();
            }

            _dbContext.Companies.Remove(companyModel);
            _dbContext.SaveChanges();

            return NoContent();
        }
        */
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var companyModel = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyModel == null)
            {
                return NotFound();
            }

            companyModel.DeletedOn = DateTime.Now;
            companyModel.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return Ok(companyModel.ToCompanyDTO());
        }

    }
}