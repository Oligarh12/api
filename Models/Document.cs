using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Document
    {
        public long Id { get; set; }
        public required string FileName { get; set; }
        public byte[]? File { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }
    }
}