namespace Spring.Application.DTO;
public class EmployeeDTO
{
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ContactNo { get; set; } = null!;
    public long CompanyId { get; set; }
}
