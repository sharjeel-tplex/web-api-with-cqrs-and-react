namespace DevTest.Domain.Entities;

public class Department : BaseAuditableEntity<int>
{
    public string Name { get; set; }
   
}
