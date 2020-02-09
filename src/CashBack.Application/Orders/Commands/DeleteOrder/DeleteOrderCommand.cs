using System;
using MediatR;

namespace CashBack.Application.Orders.Commands.DeleteOrder
{

    public class DeleteOrderCommand : IRequest
    {
        public DeleteOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}