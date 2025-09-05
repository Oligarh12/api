using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Discussion;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/discussion")]
    [ApiController]
    [Authorize]
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionRepository _repo;
        private readonly IOrderRepository _orders;
        private readonly UserManager<AppUser> _userManager;

        public DiscussionController(IDiscussionRepository repo, IOrderRepository orders, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _orders = orders;
            _userManager = userManager;
        }

        private async Task<long?> GetUserCompanyId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.CompanyId;
        }

        // GET: api/discussion/order/{orderId}/messages
        [HttpGet("order/{orderId:long}/messages")]
        public async Task<IActionResult> GetMessages([FromRoute] long orderId)
        {
            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var order = await _orders.GetByIdAsync(orderId);
            if (order == null || order.CompanyId != companyId) return NotFound();
            var discussion = await _repo.GetByOrderIdAsync(orderId);
            if (discussion == null) return NotFound();
            var messages = await _repo.GetMessagesAsync(discussion.Id);
            return Ok(messages.Select(m => m.ToDiscussionMessageDTO()));
        }

        // POST: api/discussion/order/{orderId}/messages
        [HttpPost("order/{orderId:long}/messages")]
        public async Task<IActionResult> AddMessage([FromRoute] long orderId, [FromBody] DiscussionMessageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var companyId = await GetUserCompanyId();
            if (companyId == null) return Forbid();
            var order = await _orders.GetByIdAsync(orderId);
            if (order == null || order.CompanyId != companyId) return NotFound();
            var discussion = await _repo.GetByOrderIdAsync(orderId);
            if (discussion == null) return NotFound();

            var message = new DiscussionMessage
            {
                DiscussionId = discussion.Id,
                Message = dto.Message,
                AuthorUserId = (await _userManager.GetUserAsync(User))?.Id
            };
            var created = await _repo.AddMessageAsync(message);
            return CreatedAtAction(nameof(GetMessages), new { orderId = orderId }, created.ToDiscussionMessageDTO());
        }

        // PUT: api/discussion/messages/{id}
        [HttpPut("messages/{id:long}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] long id, [FromBody] DiscussionMessageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repo.UpdateMessageAsync(id, dto.Message);
            if (updated == null) return NotFound();
            return Ok(updated.ToDiscussionMessageDTO());
        }

        // DELETE: api/discussion/messages/{id}
        [HttpDelete("messages/{id:long}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] long id)
        {
            var ok = await _repo.SoftDeleteMessageAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}

