using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Company;
using api.Models;

namespace api.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(long id); //FirstOrDefault 
        Task<Company> CreateAsync(Company companyModel);
        Task<Company?> UpdateAsync(long id, UpdateCompanyRequestDto companyDto);
        Task<Company?> DeleteAsync(long id);
    }
}