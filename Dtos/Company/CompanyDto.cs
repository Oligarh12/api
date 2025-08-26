using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public required string CompanyName { get; set; }
        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        //public List<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}