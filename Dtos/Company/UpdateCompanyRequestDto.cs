using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class UpdateCompanyRequestDto
    {
        public required string CompanyName { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}