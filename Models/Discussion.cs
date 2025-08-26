using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Discussion
    {
        public long Id { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long CompanyId { get; set; }
        public Company? Company { get; set; }
        public long OrderId { get; set; }
        public Order? Order { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}