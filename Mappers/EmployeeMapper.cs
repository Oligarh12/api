using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;
using api.Models;

namespace api.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToEmployeeDTO(this Employee employeeModel)
        {
            return new EmployeeDto
            {
                Id = employeeModel.Id,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                Patronymic = employeeModel.Patronymic,
                Position = employeeModel.Position,
                ClosestLicenseDate = employeeModel.ClosestLicenseDate,
                CompanyId = employeeModel.CompanyId

            };

        // public static Employee ToEmployeeCreate(this CreateEmployeeRequestDto employeeDto)
        // {
        //     return new Employee
        //     {
        //         CompanyName = companyDto.CompanyName,
        //         CreatedOn = DateTime.Now,
        //         IsDeleted = false
        //     };
        }
    }
}