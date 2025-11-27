namespace RDS.Core.Dtos.Review;

public class CreateReviewDto
{
    public long ServiceId { get; set; }
    public long ReviewerId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}