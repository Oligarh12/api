using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> GetAllByCompanyAsync(long companyId);
        Task<Employee?> GetByIdAsync(long id);
        Task<Employee> CreateAsync(Employee employeeModel);
        Task<Employee?> UpdateAsync(long id, Employee updated);
        Task<bool> DeleteAsync(long id);
    }
}
