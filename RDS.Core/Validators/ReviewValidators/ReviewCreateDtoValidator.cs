using FluentValidation;
using RDS.Core.Dtos.Review;

namespace RDS.Core.Validators.ReviewValidators;

public class ReviewCreateDtoValidator : AbstractValidator<CreateReviewDto>
{
    public ReviewCreateDtoValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("ServiceId deve ser maior que zero.");

        RuleFor(x => x.ReviewerId)
            .GreaterThan(0).WithMessage("ReviewerId deve ser maior que zero.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating deve estar entre 1 e 5.");

        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage("Comentário não pode exceder 1000 caracteres.");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("Data de criação não pode ser vazia.")
            .LessThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Data de criação não pode ser no futuro.");
    }
}