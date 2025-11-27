namespace RDS.Core.Dtos.ServiceOffered;

public class ServiceOfferedBaseDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? Price { get; set; }
    public bool IsActive { get; set; } = true;
    public long CategoryId { get; set; }
}
