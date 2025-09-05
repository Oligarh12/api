using System;

namespace api.Models
{
    public class DiscussionMessage
    {
        public long Id { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }
        public string? AuthorUserId { get; set; }
    }
}

