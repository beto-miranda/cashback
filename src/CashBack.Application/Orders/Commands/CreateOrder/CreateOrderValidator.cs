using FluentValidation;

namespace CashBack.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.ResellerCpf)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.Amount)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
