using System;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Employee
{
    public class UpdateEmployeeRequestDto
    {
        [Required]
        [MinLength(1)]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        public required string LastName { get; set; }

        public string Patronymic { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public required string Position { get; set; }

        public DateTime? ClosestLicenseDate { get; set; }

        [Range(1, long.MaxValue)]
        public long CompanyId { get; set; }
    }
}

