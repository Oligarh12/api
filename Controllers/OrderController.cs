using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orders;
        private readonly IEmployeeRepository _employees;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IOrderRepository orders, IEmployeeRepository employees, UserManager<AppUser> userManager)
        {
            _orders = orders;
            _employees = employees;
            _userManager = userManager;
        }

        private async Task<long?> GetUserCompanyId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.CompanyId;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var orders = await _orders.GetAllByCompanyAsync(companyId.Value);
            return Ok(orders.Select(o => o.ToOrderDTO()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();

            var order = await _orders.GetByIdAsync(id);
            if (order == null || order.CompanyId != companyId) return NotFound();
            return Ok(order.ToOrderDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var companyId = await GetUserCompanyId();
            if (companyId == null || companyId != dto.CompanyId) return Forbid();

            var model = new Order
            {
                OrderNumber = dto.OrderNumber,
                OrderName = dto.OrderName,
                CompanyId = dto.CompanyId,
                CreatedOn = System.DateTime.Now,
                ModifiedOn = System.DateTime.Now,
                IsDeleted = false
            };

            var created = await _orders.CreateAsync(model, dto.EmployeeIds);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToOrderDTO());
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateOrderRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var companyId = await GetUserCompanyId();
            if (companyId == null || companyId != dto.CompanyId) return Forbid();

            var updatedModel = new Order
            {
                OrderNumber = dto.OrderNumber,
                OrderName = dto.OrderName,
                CompanyId = dto.CompanyId,
                ModifiedOn = System.DateTime.Now,
                IsDeleted = dto.IsDeleted
            };

            var updated = await _orders.UpdateAsync(id, updatedModel, dto.EmployeeIds);
            if (updated == null) return NotFound();
            return Ok(updated.ToOrderDTO());
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();

            var existing = await _orders.GetByIdAsync(id);
            if (existing == null || existing.CompanyId != companyId) return NotFound();

            var ok = await _orders.SoftDeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}

