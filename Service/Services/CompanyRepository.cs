using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DataContext;
using Service.Dtos;
using Service.Interfaces;

namespace Service.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext dbContext;
        public CompanyRepository(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext;
        }
        public async Task CreateCompanyAsync(CreateCompanyDto company)
        {
            var companyCreate = new Company()
            {
                Address = company.Address,
                Email = company.Email,
                Name = company.Name,
                Phone = company.Phone,
                Id = Guid.NewGuid()
            };

            await dbContext.Companies.AddAsync(companyCreate);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var companies = await dbContext.Companies.ToListAsync();
            return companies;
        }

        public async Task<Company> GetCompanyByIdAsync(Guid companyId)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(i => i.Id == companyId);
            return company;
        }

        public async Task<string> UpdateCompanyByIdAsync(Guid companyId,UpdateCompanyDto newCompany)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(i => i.Id == companyId);

            company.Address = newCompany.Address;
            company.Email = newCompany.Email;
            company.Name = newCompany.Name;
            company.Phone = newCompany.Phone;
            await dbContext.SaveChangesAsync();

            return "Updated";
        }

        public async Task<string> DeleteCompanyByIdAsync(Guid companyId)
        {
            var res = await dbContext.Companies.FirstOrDefaultAsync<Company>(i => i.Id == companyId);
            if (res != null)
            {
                dbContext.Companies.Remove(res);
                await dbContext.SaveChangesAsync();
                return "deleted";
            }
            else return "didn`t delete";
        }

        
    }
}
