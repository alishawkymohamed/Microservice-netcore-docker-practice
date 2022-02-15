using AutoMapper;
using EventBusrabbitMQ.Events;
using Orders.Application.Commands;

namespace Orders.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>()
                .ReverseMap();
        }
    }
}
