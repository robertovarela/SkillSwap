using FluentValidation;
using RDS.Core.Dtos.Message;

namespace RDS.Core.Validators.MessageValidators;

public class MessageCreateDtoValidator : AbstractValidator<MessageCreateDto>
{
    public MessageCreateDtoValidator()
    {
        RuleFor(x => x.SenderId)
            .GreaterThan(0);

        RuleFor(x => x.ReceiverId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("O destinatário é obrigatório.")
            .NotEqual(x => x.SenderId).WithMessage("O destinatário deve ser diferente do remetente.");

        RuleFor(x => x.Content)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O conteúdo da mensagem é obrigatório.")
            .MaximumLength(1000).WithMessage("O conteúdo não pode exceder 1000 caracteres.");
    }
}