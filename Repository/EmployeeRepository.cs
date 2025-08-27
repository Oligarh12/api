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
        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}