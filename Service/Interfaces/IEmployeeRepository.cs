using Domain.Entities;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task CreateEmployeeAsync(CreateEmployeeDto employee);
        public Task<List<GetEmployeeDto>> GetAllEmployeesAsync();
        public Task<GetEmployeeDto> GetEmployeeByIdAsync(Guid id);
        public Task<string> DeleteEmployeeByIdAsync(Guid employeeId);
        public Task<string> UpdateEmployeeByIdAsync(Guid employeeId, UpdateEmployeeDto newEmployee);

    }
}
