using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Application.Common.Exceptions;
using CashBack.Application.Orders.Models;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderModel>>
    {
        private readonly IOrderRepository orderRespository;
        private readonly IResellerRepository resellerRepository;
        private readonly IMapper mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRespository, IResellerRepository resellerRepository, IMapper mapper)
        {
            this.orderRespository = orderRespository;
            this.resellerRepository = resellerRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var reseller = await resellerRepository.GetByIdAsync(request.ResellerId);

            if (reseller == null)
            {
                throw new NotFoundException(nameof(Reseller), request.ResellerId);
            }
            var orders =  await orderRespository.GetAllAsync(reseller.Id);
            return mapper.Map<List<OrderModel>>(orders);
        }

       
    }
}