using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Order
    {
        public long Id { get; set; }
        public required string OrderNumber { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
        public long CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Discussion? Discussion { get; set; }
    }
}
