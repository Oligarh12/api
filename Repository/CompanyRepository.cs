using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Company;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DBContext _dbContext;
        public CompanyRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company> CreateAsync(Company companyModel)
        {
            await _dbContext.Companies.AddAsync(companyModel);
            await _dbContext.SaveChangesAsync();
            return companyModel;
        }

        public async Task<Company?> DeleteAsync(long id)
        {
            var companyModel = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyModel == null)
            {
                return null;
            }

            companyModel.DeletedOn = DateTime.Now;
            companyModel.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return companyModel;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(long id)
        {
            return await _dbContext.Companies.FindAsync(id);
        }

        public async Task<Company?> UpdateAsync(long id, UpdateCompanyRequestDto updateDto)
        {
            var companyModel = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (companyModel == null)
            {
                return null;
            }

            companyModel.CompanyName = updateDto.CompanyName;
            companyModel.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();
            return companyModel;
        }
    }
}