using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

namespace FirstAPIProject_CRUD_.Controllers
{
    [Route("api/Staffs")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository staffRepository;
        public StaffsController(IStaffRepository staffRepository)
        {
            this.staffRepository = staffRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStaffs()
        {
            var staffs = await staffRepository.GetAllStaffsAsync();
            return Ok(staffs);
        }

        [HttpPost]
        public async Task<IActionResult> CreatStaff([FromForm] CreateStaffDto createStaffDto)
        {
            await staffRepository.CreateStaffAsync(createStaffDto);
            return Ok("Created");
        }

        [HttpPost("EployeeToStaff")]
        public async Task<IActionResult> AddEmployeesToStaff([FromForm] Guid staffId, [FromForm] List<Guid> employeesIds)
        {
            await staffRepository.AddEmployeesToStaffAsync(staffId, employeesIds);
            return Ok("Created");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaff([FromForm] Guid StaffId)
        {
            await staffRepository.DeleteStaffAsync(StaffId);
            return Ok("Deleted");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStaff([FromForm] Guid StaffId, [FromForm] CreateStaffDto staffDto)
        {
            await staffRepository.UpdateStaffAsync(StaffId, staffDto);
            return Ok("Updated");
        }

    }
}
