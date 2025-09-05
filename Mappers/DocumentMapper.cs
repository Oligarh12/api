using api.Dtos.Document;
using api.Models;

namespace api.Mappers
{
    public static class DocumentMapper
    {
        public static DocumentDto ToDocumentDTO(this Document model)
        {
            return new DocumentDto
            {
                Id = model.Id,
                FileName = model.FileName,
                File = model.File,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                DeletedOn = model.DeletedOn,
                IsDeleted = model.IsDeleted,
                DiscussionId = model.DiscussionId
            };
        }
    }
}

