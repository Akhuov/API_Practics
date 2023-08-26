using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace FirstAPIProject_CRUD_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatEmployee([FromForm] CreateEmployeeDto createEmployeeDto)
        {
            await employeeRepository.CreateEmployeeAsync(createEmployeeDto);
            return Ok("Created");
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeId(Guid employeeId)
        {
            var employee = await employeeRepository.GetEmployeeByIdAsync(employeeId);
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeById([FromForm] Guid id)
        {
            var status = await employeeRepository.DeleteEmployeeByIdAsync(id);
            return Ok(status);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompanyById([FromForm] Guid id, [FromForm] UpdateEmployeeDto updateCompanyDto)
        {
            var company = await employeeRepository.UpdateEmployeeByIdAsync(id, updateCompanyDto);
            return Ok(company);
        }
    }
}
