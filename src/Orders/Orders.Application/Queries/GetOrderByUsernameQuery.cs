using MediatR;
using Orders.Application.Responses;
using System.Collections.Generic;

namespace Orders.Application.Queries
{
    public class GetOrderByUsernameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string Username { get; set; }

        public GetOrderByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
