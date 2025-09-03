using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;

namespace api.Dtos.Company
{
    public class CompanyDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Назва компанії повинна містити хоча б 1 символ")]
        public required string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        //public List<Discussion> Discussions { get; set; } = new List<Discussion>();
        public List<EmployeeDto> Employees { get; set; }
    }
}