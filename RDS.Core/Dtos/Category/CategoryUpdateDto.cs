namespace RDS.Core.Dtos.Category;

public class CategoryUpdateDto : CategoryBaseDto
{
    public long Id { get; init; }
    public bool HasAssociatedServices { get; set; }
}
