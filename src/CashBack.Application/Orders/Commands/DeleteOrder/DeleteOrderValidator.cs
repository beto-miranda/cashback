using FluentValidation;

namespace CashBack.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotNull();
        }
    }
}