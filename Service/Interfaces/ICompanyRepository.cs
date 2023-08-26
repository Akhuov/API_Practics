using Domain.Entities;
using Service.Dtos;

namespace Service.Interfaces
{
    public interface ICompanyRepository
    {
        public Task CreateCompanyAsync(CreateCompanyDto company);
        public Task<Company> GetCompanyByIdAsync(Guid company);
        public Task<List<Company>> GetAllAsync();
        public Task<string> DeleteCompanyByIdAsync(Guid companyId);
        public Task<string> UpdateCompanyByIdAsync(Guid companyId, UpdateCompanyDto newCompany);
    }
}
