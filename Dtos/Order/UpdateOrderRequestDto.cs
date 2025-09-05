using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Order
{
    public class UpdateOrderRequestDto
    {
        [Required]
        [MinLength(1)]
        public required string OrderNumber { get; set; }
        public string OrderName { get; set; } = string.Empty;
        [Range(1, long.MaxValue)]
        public long CompanyId { get; set; }
        public List<long> EmployeeIds { get; set; } = new List<long>();
        public bool IsDeleted { get; set; } = false;
    }
}

