using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext _dbContext;
        public EmployeeRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(long id)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Employee> CreateAsync(Employee employeeModel)
        {
            await _dbContext.Employees.AddAsync(employeeModel);
            await _dbContext.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<Employee?> UpdateAsync(long id, Employee updated)
        {
            var existing = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null)
            {
                return null;
            }

            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.Patronymic = updated.Patronymic;
            existing.Position = updated.Position;
            existing.ClosestLicenseDate = updated.ClosestLicenseDate;
            existing.CompanyId = updated.CompanyId;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null)
            {
                return false;
            }

            if (existing.IsDeleted)
            {
                return true; // already soft-deleted
            }

            existing.IsDeleted = true;
            existing.DeletedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
