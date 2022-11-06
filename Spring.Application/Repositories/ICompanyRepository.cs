using Spring.Application.DTO;

namespace Spring.Application.Repositories;
public interface ICompanyRepository
{
    Task<Company> Create(Company company, CancellationToken cancellationToken = default);
    Task<IEnumerable<Company>> GetAll(CancellationToken cancellationToken = default);
    Task<IEnumerable<Company>> GetAllCompanies(CancellationToken cancellationToken = default);
    Task<IEnumerable<CompanyEmployeeGroupDTO>> GetCompanyAndEmployeeCount(CancellationToken cancellationToken = default);
}
