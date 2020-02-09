using System;
using System.Collections.Generic;
using CashBack.Application.Common.Mapping;
using CashBack.Application.Orders.Models;
using MediatR;

namespace CashBack.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<OrderModel>>
    {
        public GetOrdersQuery(Guid resellerId)
        {
            ResellerId = resellerId;
        }

        public Guid ResellerId { get; set; }
    }
}