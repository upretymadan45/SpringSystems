namespace Spring.Domain;
public class Entity
{
    public long Id { get; set; }
    public DateTime AddedOn { get; set; } = DateTime.Now;
    public DateTime? UpdatedOn { get; set; }
    public string AddedBy { get; set; }
}
