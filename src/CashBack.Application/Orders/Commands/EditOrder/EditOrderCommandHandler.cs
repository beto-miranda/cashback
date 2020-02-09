using System;
using System.Threading;
using System.Threading.Tasks;
using CashBack.Application.Common.Exceptions;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Orders.Commands.EditOrder
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand>
    {
        private readonly IOrderRepository orderRepository;

        public EditOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            if (order.Status != OrderStatus.Validation)
            {
                throw new BusinessValidationException(nameof(Order.Status), "Only orders in validation state can be edited");
            }

            order.Amount = request.Amount;
            order.Updated = DateTime.UtcNow;
            await orderRepository.UpdateAsync(order);

            return Unit.Value;

        }
    }
}