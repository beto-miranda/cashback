using System;
using CashBack.Application.Common.Mapping;
using CashBack.Domain.Entities;
using MediatR;

namespace CashBack.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Order>, IMapTo<Order>
    {
        public Guid? Id { get; set; }
        public Decimal? Amount { get; set; }
        public string ResellerCpf { get; set; }
    }
}
