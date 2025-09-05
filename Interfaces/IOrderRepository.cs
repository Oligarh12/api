using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllByCompanyAsync(long companyId);
        Task<Order?> GetByIdAsync(long id);
        Task<Order> CreateAsync(Order order, IEnumerable<long> employeeIds);
        Task<Order?> UpdateAsync(long id, Order updated, IEnumerable<long> employeeIds);
        Task<bool> SoftDeleteAsync(long id);
    }
}

