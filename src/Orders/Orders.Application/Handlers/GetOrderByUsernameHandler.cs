using MediatR;
using Orders.Application.Mapper;
using Orders.Application.Queries;
using Orders.Application.Responses;
using Orders.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Application.Handlers
{
    public class GetOrderByUsernameHandler : IRequestHandler<GetOrderByUsernameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository repository;

        public GetOrderByUsernameHandler(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrderByUsernameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await repository.GetOrdersByUserName(request.Username);

            return OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(orderList);
        }
    }
}
