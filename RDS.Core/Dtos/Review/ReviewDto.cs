namespace RDS.Core.Dtos.Review;

public record ReviewDto(
    long Id,
    long ServiceId,
    long ReviewerId,
    int Rating,
    string? Comment,
    DateTimeOffset CreatedAt
);