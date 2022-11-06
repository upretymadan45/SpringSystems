namespace Spring.Domain;
public class Employee : Entity
{
    public Employee()
    {

    }
    public Employee(string firstName, string middleName, string lastName,string address, string email, string contactNo,long companyId, string addedBy)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Address = address;
        Email = email;
        ContactNo = contactNo;
        IsActive = true;
        CompanyId = companyId;
        AddedOn = DateTime.Now;
        AddedBy = addedBy;
    }
    public string FirstName { get; private set; }
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string ContactNo { get; private set; }
    public bool IsActive { get; private set; } = true;
    public long CompanyId { get; private set; }
    public Company Company { get; set; }

    public string GetFullName()
    {
        if (string.IsNullOrWhiteSpace(MiddleName)) return $"{FirstName} {LastName}";
        return $"{FirstName} {MiddleName} {LastName}";
    }
}
