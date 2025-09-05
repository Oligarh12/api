using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Patronymic { get; set; } = string.Empty;
        public required string Position { get; set; }
        public DateTime? ClosestLicenseDate { get; set; }
        public long CompanyId { get; set; }
        public required Company Company { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
