using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class DeleteCompanyRequestDto
    {
        public DateTime DeletedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = true;
    }
}