using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Discussion;

namespace api.Dtos.Order
{
    public class OrderDto
    {
        public long Id { get; set; }
        public required string OrderNumber { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        // public List<DiscussionDto> Discussions { get; set; } = new List<DiscussionDto>();
    }
}