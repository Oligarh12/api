using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Employee
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Patronymic { get; set; } = string.Empty;
        public required string Position { get; set; }
        public DateTime? ClosestLicenseDate { get; set; }
        public long CompanyId { get; set; }
    }
}