using System;
using MediatR;

namespace CashBack.Application.Orders.Commands.EditOrder
{

    public class EditOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}