using System.Linq;
using api.Dtos.Order;
using api.Mappers;
using api.Models;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDTO(this Order model)
        {
            return new OrderDto
            {
                Id = model.Id,
                OrderNumber = model.OrderNumber,
                OrderName = model.OrderName,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                DeletedOn = model.DeletedOn,
                IsDeleted = model.IsDeleted,
                CompanyId = model.CompanyId,
                Employees = model.Employees.Select(e => e.ToEmployeeDTO()).ToList(),
                Discussion = model.Discussion?.ToDiscussionDTO()
            };
        }
    }
}

