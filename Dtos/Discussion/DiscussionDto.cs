using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Company;
using api.Dtos.Document;
using api.Dtos.Order;

namespace api.Dtos.Discussion
{
    public class DiscussionDto
    {
        public long Id { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long CompanyId { get; set; }
        public long OrderId { get; set; }
        // public OrderDto? Order { get; set; }
        // public List<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
    }
}