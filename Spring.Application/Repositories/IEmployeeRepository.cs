namespace Spring.Application.Repositories;
public interface IEmployeeRepository
{
    Task<Employee> Create(Employee employee, CancellationToken cancellationToken = default);
    Task<IEnumerable<Employee>> GetEmployees(CancellationToken cancellationToken = default);
}
