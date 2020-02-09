using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Application.Common.Exceptions;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IResellerRepository resellerRepository;
        private readonly IMapper mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IResellerRepository resellerRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.resellerRepository = resellerRepository;
            this.mapper = mapper;
        }
        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await orderRepository.GetAsync(request.Id.Value);
            if(existingOrder != null)
            {
                throw new AlreadyExistsException(nameof(Order), request.Id);
            }
            var order = mapper.Map<Order>(request);
            var reseller = await resellerRepository.GetByCpfAsync(request.ResellerCpf);
            if(reseller == null)
            {
                throw new NotFoundException(nameof(Reseller), request.ResellerCpf);
            }
            order.Reseller = reseller;
            order.Created = order.Updated = DateTime.Now;

            if(reseller.CPF == "15350946056")
            {
                order.ApplyCashback();
            }

            await orderRepository.CreateAsync(order);
            order.Reseller = null;
            return order;
        }
    }
}
