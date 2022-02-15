using MediatR;
using Orders.Application.Commands;
using Orders.Application.Mapper;
using Orders.Application.Responses;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Application.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository orderRepository;

        public CheckoutOrderHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = OrderMapper.Mapper.Map<Order>(request);

            if (entity == null)
                throw new Exception("Not Mapped Order");

            var newOrder = await orderRepository.AddAsunc(entity);
            return OrderMapper.Mapper.Map<OrderResponse>(newOrder);
        }
    }
}
