using AutoMapper;
using Basket.API.Entities;
using EventBusrabbitMQ.Events;

namespace Basket.API.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
