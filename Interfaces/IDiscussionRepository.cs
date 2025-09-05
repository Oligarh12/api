using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IDiscussionRepository
    {
        Task<Discussion?> GetByOrderIdAsync(long orderId);
        Task<List<DiscussionMessage>> GetMessagesAsync(long discussionId);
        Task<DiscussionMessage> AddMessageAsync(DiscussionMessage message);
        Task<DiscussionMessage?> UpdateMessageAsync(long id, string text);
        Task<bool> SoftDeleteMessageAsync(long id);
        Task<Document> AddDocumentAsync(Document doc);
        Task<bool> SoftDeleteDocumentAsync(long id);
    }
}

