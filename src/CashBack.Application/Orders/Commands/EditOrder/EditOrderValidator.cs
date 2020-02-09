using FluentValidation;

namespace CashBack.Application.Orders.Commands.EditOrder
{
    public class EditOrderValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderValidator()
        {
            RuleFor(x => x.Amount)
                .NotNull()
                .GreaterThan(0);
        }
    }
}