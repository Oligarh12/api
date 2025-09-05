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
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly DBContext _dbContext;
        public DiscussionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discussion?> GetByOrderIdAsync(long orderId)
        {
            return await _dbContext.Discussions
                .Include(d => d.Messages.Where(m => !m.IsDeleted))
                .Include(d => d.Documents.Where(doc => !doc.IsDeleted))
                .FirstOrDefaultAsync(d => d.OrderId == orderId && !d.IsDeleted);
        }

        public async Task<List<DiscussionMessage>> GetMessagesAsync(long discussionId)
        {
            return await _dbContext.DiscussionMessages
                .Where(m => m.DiscussionId == discussionId && !m.IsDeleted)
                .OrderBy(m => m.CreatedOn)
                .ToListAsync();
        }

        public async Task<DiscussionMessage> AddMessageAsync(DiscussionMessage message)
        {
            message.CreatedOn = DateTime.Now;
            message.ModifiedOn = DateTime.Now;
            message.IsDeleted = false;
            await _dbContext.DiscussionMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<DiscussionMessage?> UpdateMessageAsync(long id, string text)
        {
            var existing = await _dbContext.DiscussionMessages.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (existing == null) return null;
            existing.Message = text;
            existing.ModifiedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> SoftDeleteMessageAsync(long id)
        {
            var existing = await _dbContext.DiscussionMessages.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return false;
            if (existing.IsDeleted) return true;
            existing.IsDeleted = true;
            existing.DeletedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Document> AddDocumentAsync(Document doc)
        {
            doc.CreatedOn = DateTime.Now;
            doc.ModifiedOn = DateTime.Now;
            doc.IsDeleted = false;
            await _dbContext.Documents.AddAsync(doc);
            await _dbContext.SaveChangesAsync();
            return doc;
        }

        public async Task<bool> SoftDeleteDocumentAsync(long id)
        {
            var existing = await _dbContext.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return false;
            if (existing.IsDeleted) return true;
            existing.IsDeleted = true;
            existing.DeletedOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

