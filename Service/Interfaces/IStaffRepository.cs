using Domain.Entities;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStaffRepository
    {
        public Task CreateStaffAsync(CreateStaffDto createStaffDto);
        public Task<Staff> GetStaffAsync(Guid staffId);
        public Task DeleteStaffAsync(Guid staffId);
        public Task UpdateStaffAsync(Guid staffId, CreateStaffDto createStaffDto);
        public Task<List<Staff>> GetAllStaffsAsync();
        public Task<List<Employee>> GetAllEmployeesById(Guid staffId);
        public Task AddEmployeesToStaffAsync(Guid staffId,List<Guid> employeesIds);
    }
}
