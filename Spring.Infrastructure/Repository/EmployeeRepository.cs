namespace Spring.Infrastructure.Repository;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ILogger<EmployeeRepository> _logger;
    private readonly ISpringDbConnection _springDbConnection;

    public EmployeeRepository(ILogger<EmployeeRepository> logger,
        ISpringDbConnection springDbConnection)
    {
        _logger = logger;
        _springDbConnection = springDbConnection;
    }
    public async Task<Employee> Create(Employee employee, CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();
            
            var query = @"Insert into employee(FirstName,MiddleName,LastName,Address,Email,ContactNo,IsActive,CompanyId,AddedOn,AddedBy)
                          values(@FirstName,@MiddleName,@LastName,@Address,@Email,@ContactNo,@IsActive,@CompanyId,@AddedOn,@AddedBy)";
            await conn.ExecuteAsync(query, employee);

            return employee;
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not create employee {@employee}", employee);
            throw;
        }
    }

    public async Task<IEnumerable<Employee>> GetEmployees(CancellationToken cancellationToken = default)
    {
        try
        {
            using var conn = _springDbConnection.GetDbConnection();

            var query = @"Select * from Employee as e inner join company as c on e.CompanyId = c.Id where e.IsActive=@IsActive order by e.FirstName";
            var employees = await conn.QueryAsync<Employee, Company, Employee>(query, (employee, company) =>
            {
                employee.Company = company;
                return employee;
            }, new {IsActive = true},splitOn:"CompanyId");

            return employees;
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Could not get all the employees");
            throw;
        }
    }
}
