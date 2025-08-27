using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Company;
using api.Interfaces;
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
        private readonly ICompanyRepository _companyRepo;
        public CompanyController(DBContext dbContext, ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyRepo.GetAllAsync();
            var companyDto = companies.Select(s => s.ToCompanyDTO());
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById([FromRoute] long id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
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

            await _companyRepo.CreateAsync(companyModel);

            return CreatedAtAction(nameof(GetCompanyById), new { id = companyModel.Id }, companyModel.ToCompanyDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateCompanyRequestDto updateDto)
        {
            var companyModel = await _companyRepo.UpdateAsync(id, updateDto);

            if (companyModel == null)
            {
                return NotFound();
            }

            return Ok(companyModel.ToCompanyDTO());
        }

        /*
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] long id)
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
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var companyModel = await _companyRepo.DeleteAsync(id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return Ok(companyModel.ToCompanyDTO());
        }

    }
}