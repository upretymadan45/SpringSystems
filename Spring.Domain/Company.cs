namespace Spring.Domain;
public class Company : Entity
{
    public Company()
    {

    }
    public Company(string name, string address, string addedBy)
    {
        Name = name;
        Address = address;
        IsActive = true;    
        AddedOn = DateTime.Now;
        AddedBy = addedBy;
    }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public bool IsActive { get; private set; } = true;
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
