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
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _dbContext;
        public OrderRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllByCompanyAsync(long companyId)
        {
            return await _dbContext.Orders
                .Include(o => o.Employees.Where(e => !e.IsDeleted))
                .Include(o => o.Discussion)!
                    .ThenInclude(d => d!.Messages.Where(m => !m.IsDeleted))
                .Where(o => o.CompanyId == companyId && !o.IsDeleted)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(long id)
        {
            return await _dbContext.Orders
                .Include(o => o.Employees.Where(e => !e.IsDeleted))
                .Include(o => o.Discussion)!
                    .ThenInclude(d => d!.Messages.Where(m => !m.IsDeleted))
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<Order> CreateAsync(Order order, IEnumerable<long> employeeIds)
        {
            // attach employees from same company
            var employees = await _dbContext.Employees
                .Where(e => employeeIds.Contains(e.Id) && !e.IsDeleted && e.CompanyId == order.CompanyId)
                .ToListAsync();

            order.Employees = employees;
            order.CreatedOn = DateTime.Now;
            order.IsDeleted = false;

            await _dbContext.Orders.AddAsync(order);

            // create discussion chat
            var discussion = new Discussion
            {
                CompanyId = order.CompanyId,
                Order = order,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                ModifiedOn = DateTime.Now
            };
            await _dbContext.Discussions.AddAsync(discussion);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateAsync(long id, Order updated, IEnumerable<long> employeeIds)
        {
            var existing = await _dbContext.Orders
                .Include(o => o.Employees)
                .Include(o => o.Discussion)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            if (existing == null) return null;

            existing.OrderNumber = updated.OrderNumber;
            existing.OrderName = updated.OrderName;
            existing.ModifiedOn = DateTime.Now;

            // update employees list with validation
            var employees = await _dbContext.Employees
                .Where(e => employeeIds.Contains(e.Id) && !e.IsDeleted && e.CompanyId == existing.CompanyId)
                .ToListAsync();
            existing.Employees = employees;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var existing = await _dbContext.Orders
                .Include(o => o.Discussion)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (existing == null) return false;

            if (existing.IsDeleted) return true;

            existing.IsDeleted = true;
            existing.DeletedOn = DateTime.Now;

            if (existing.Discussion != null)
            {
                existing.Discussion.IsDeleted = true;
                existing.Discussion.DeletedOn = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

