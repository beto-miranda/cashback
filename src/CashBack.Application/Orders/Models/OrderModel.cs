using System;
using CashBack.Application.Common.Mapping;
using CashBack.Domain.Entities;

namespace CashBack.Application.Orders.Models
{
    public class OrderModel : IMapFrom<Order>
        {
            public Guid Id { get; set; }
            public Decimal Amount { get; set; }
            public OrderStatus Status { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public decimal? CashbackPercentage { get; set; }
            public decimal? Cashback { get; set; }
        }
}
