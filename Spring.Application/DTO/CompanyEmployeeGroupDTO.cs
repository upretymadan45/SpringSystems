namespace Spring.Application.DTO;
public class CompanyEmployeeGroupDTO
{
    public long CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string CompanyAddress { get; set; } = null!;
    public int TotalEmployees { get; set; }
}
