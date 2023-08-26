using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DataContext;
using Service.Dtos;
using Service.Interfaces;


namespace Service.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext dbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext;
        }
        public async Task CreateEmployeeAsync(CreateEmployeeDto employee)
        {
            var employeeCreate = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Id = Guid.NewGuid()
            };

            await dbContext.Employees.AddAsync(employeeCreate);
            await dbContext.SaveChangesAsync();
        }
        public async Task<string> DeleteEmployeeByIdAsync(Guid employeeId)
        {
            var res = await dbContext.Employees.FirstOrDefaultAsync<Employee>(i => i.Id == employeeId);
            if (res != null)
            {
                dbContext.Employees.Remove(res);
                await dbContext.SaveChangesAsync();
                return "deleted";
            }
            else return "didn`t delete";
        }
        public async Task<List<GetEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await dbContext.Employees.Include(x => x.Company).ToListAsync();
            var employeesDto = new List<GetEmployeeDto>();
            


            foreach (var employee in employees)
            {
                var company = employee.Company;
                String companysName=null;
                if (company!=null)
                {
                    companysName =company.Name;
                }
               
                employeesDto.Add(new GetEmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    CompanyName = companysName
                });
            }
            return employeesDto;
        }
        public async Task<GetEmployeeDto> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await dbContext.Employees.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var company = employee.Company;
                String companysName = null;
                if (company != null)
                {
                    companysName = company.Name;
                }
                var employeeDto = new GetEmployeeDto()
                {
                    Id = id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Phone = employee.Phone,
                    CompanyName = companysName,
                    Email = employee.Email
                };
                return employeeDto;
            }
            else return null;
        }
        public async Task<string> UpdateEmployeeByIdAsync(Guid employeeId, UpdateEmployeeDto newEmployee)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(i => i.Id == employeeId);
            if (employee != null)
            {
                employee.FirstName = newEmployee.FirstName;
                employee.LastName = newEmployee.LastName;
                employee.Phone = newEmployee.Phone;
                employee.Email = newEmployee.Email;

                await dbContext.SaveChangesAsync();
                return "Updated";
            }
            else
            {
                return "Error";
            }

        }
    }
}
