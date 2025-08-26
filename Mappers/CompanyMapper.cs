using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Company;
using api.Models;

namespace api.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDto ToCompanyDTO(this Company companyModel)
        {
            return new CompanyDto
            {
                Id = companyModel.Id,
                CompanyName = companyModel.CompanyName,
                CreatedOn = companyModel.CreatedOn,
                ModifiedOn = companyModel.ModifiedOn,
                DeletedOn = companyModel.DeletedOn,
                IsDeleted = companyModel.IsDeleted
            };
        }

        public static Company ToCompanyCreate(this CreateCompanyRequestDto companyDto)
        {
            return new Company
            {
                CompanyName = companyDto.CompanyName,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };
        }
    }
}