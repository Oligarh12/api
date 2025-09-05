using System.Linq;
using api.Dtos.Discussion;
using api.Models;

namespace api.Mappers
{
    public static class DiscussionMapper
    {
        public static DiscussionDto ToDiscussionDTO(this Discussion model)
        {
            return new DiscussionDto
            {
                Id = model.Id,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                DeletedOn = model.DeletedOn,
                IsDeleted = model.IsDeleted,
                CompanyId = model.CompanyId,
                OrderId = model.OrderId,
                Documents = model.Documents.Select(d => d.ToDocumentDTO()).ToList(),
                Messages = model.Messages.Where(m => !m.IsDeleted).Select(m => m.ToDiscussionMessageDTO()).ToList()
            };
        }

        public static DiscussionMessageDto ToDiscussionMessageDTO(this DiscussionMessage message)
        {
            return new DiscussionMessageDto
            {
                Id = message.Id,
                Message = message.Message,
                CreatedOn = message.CreatedOn,
                ModifiedOn = message.ModifiedOn,
                DeletedOn = message.DeletedOn,
                IsDeleted = message.IsDeleted,
                DiscussionId = message.DiscussionId,
                AuthorUserId = message.AuthorUserId
            };
        }
    }
}

