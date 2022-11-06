using Spring.Application.DTO;

namespace Spring.Infrastructure.Repository;
public class CompanyRepository : ICompanyRepository
{
    private readonly ILogger<CompanyRepository> _logger;
    private readonly ISpringDbConnection _springDbConnection;

    public CompanyRepository(ILogger<CompanyRepository> logger,
        ISpringDbConnection springDbConnection)
    {
        _logger = logger;
        _springDbConnection = springDbConnection;
    }
    public async Task<Company> Create(Company company, CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();
            
            var query = @"Insert into company(Name,Address,IsActive,AddedOn,AddedBy) 
                        values(@Name,@Address,@IsActive,@AddedOn,@AddedBy)";
            await conn.ExecuteAsync(query, company);
            
            return company;
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not create company {@compay}", company);
            throw;
        }
    }

    public async Task<IEnumerable<Company>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();

            var query = @"Select c.Id,c.Name,c.Address,e.Id from company as c 
                        left join employee as e on c.Id = e.CompanyId where c.IsActive=@IsActive order by c.Name";
            var companies = await conn.QueryAsync<Company, Employee, Company>(query, (company, employee) =>
            {
                company.Employees.Add(employee);
                return company;
            }, new {IsActive = true},splitOn:"CompanyId");

            return companies;
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not fetch all companies");
            throw;
        }
    }

    public async Task<IEnumerable<Company>> GetAllCompanies(CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();

            var query = "Select Id, Name from Company where IsActive = @IsActive order by Name";

            return await conn.QueryAsync<Company>(query, new { IsActive = true });
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not load all companies");
            throw;
        }
    }

    public async Task<IEnumerable<CompanyEmployeeGroupDTO>> GetCompanyAndEmployeeCount(CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();

            var query = @"Select c.Id as CompanyId,c.Name as CompanyName,c.Address as CompanyAddress, Count(e.Id) as [TotalEmployees] from Company as c 
                        left join Employee as e on c.Id = e.CompanyId 
                        where c.IsActive=@IsActive
                        group by c.Name,c.Id,c.Address order by c.Name";
            return await conn.QueryAsync<CompanyEmployeeGroupDTO>(query, new { IsActive = true });
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not get company and its total employees");
            throw;
        }
    }
}
