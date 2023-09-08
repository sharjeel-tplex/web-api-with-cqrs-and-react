namespace DevTest.Domain.Entities;

public class Employee : BaseAuditableEntity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public DateTime DateOfBirth { get; set; }
   
}
