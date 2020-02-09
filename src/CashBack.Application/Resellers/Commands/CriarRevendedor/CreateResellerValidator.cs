using FluentValidation;

namespace CashBack.Application.Revendedor.Commands.CriarRevendedor
{
    public class CreateResellerValidator : AbstractValidator<CreateResellerCommand>
    {
        public CreateResellerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(300);
            
            RuleFor(x => x.CPF)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
