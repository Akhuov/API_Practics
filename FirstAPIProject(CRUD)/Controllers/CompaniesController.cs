using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace FirstAPIProject_CRUD_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        public readonly ICompanyRepository companyRepository;
        public CompaniesController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatCompany([FromForm] CreateCompanyDto createCompanyDto)
        {
            await companyRepository.CreateCompanyAsync(createCompanyDto);
            return Ok("Created");
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyId(Guid companyId)
        {
            var company = await companyRepository.GetCompanyByIdAsync(companyId);
            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await companyRepository.GetAllAsync();
            return Ok(companies);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyById([FromForm] Guid id)
        {
            var status = await companyRepository.DeleteCompanyByIdAsync(id);
            return Ok(status);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCompanyById([FromForm] Guid id, [FromForm] UpdateCompanyDto updateCompanyDto)
        {
            var company = await companyRepository.UpdateCompanyByIdAsync(id, updateCompanyDto);
            return Ok(company);
        }
    }
}
