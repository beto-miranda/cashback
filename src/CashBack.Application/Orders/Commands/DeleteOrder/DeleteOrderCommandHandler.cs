using System;
using System.Threading;
using System.Threading.Tasks;
using CashBack.Application.Common.Exceptions;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.OrderId);

            if(order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            if (order.Status != OrderStatus.Validation)
            {
                throw new BusinessValidationException(nameof(Order.Status), "Only orders in validation state can be deleted");
            }

            await orderRepository.DeleteAsync(order.Id);
            return Unit.Value;
        }
    }
}