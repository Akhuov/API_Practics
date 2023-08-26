using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DataContext;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext dbContext;
        public StaffRepository(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext;
        }
        
        public async Task AddEmployeesToStaffAsync(Guid staffId, List<Guid> employeesIds)
        {
            var staff = await dbContext.Staffs
                .FirstOrDefaultAsync(s=>s.Id == staffId);

            if (staff != null)
            {
                foreach (var i in employeesIds)
                {
                    var employee = await dbContext.Employees
                        .FirstOrDefaultAsync(e => e.Id == i);
                    if(employee!=null)
                        staff.Employees.Add(employee);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task CreateStaffAsync(CreateStaffDto createStaffDto)
        {
            var staffCreate = new Staff()
            {
                Id = Guid.NewGuid(),
                Name = createStaffDto.Name
            };
            await dbContext.Staffs.AddAsync(staffCreate);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(Guid staffId)
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(i => i.Id == staffId);
            if (staff is not null)
            {
                dbContext.Staffs.Remove(staff);
                await dbContext.SaveChangesAsync();
                
            }
        }

        public async Task<List<Employee>> GetAllEmployeesById(Guid staffId)
        {
            var staff = await dbContext.Staffs.Include(i => i.Employees)
                .FirstOrDefaultAsync(s => s.Id == staffId);

            return staff.Employees.ToList();
        }
    
        public async Task<List<Staff>> GetAllStaffsAsync()
        {
            List<Staff> staffs = await dbContext.Staffs.Include(x=>x.Employees).ToListAsync(); 
            return staffs;
        }

        public async Task<Staff> GetStaffAsync(Guid staffId)
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(s => s.Id == staffId);
            return staff;
        }

        public async Task UpdateStaffAsync(Guid staffId, CreateStaffDto createStaffDto)
        {
            var staff = await dbContext.Staffs.FirstOrDefaultAsync(i => i.Id == staffId);
            if (staff != null)
            {
                staff.Name = createStaffDto.Name;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
