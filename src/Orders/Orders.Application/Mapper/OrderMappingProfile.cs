using AutoMapper;
using Orders.Application.Commands;
using Orders.Application.Responses;
using Orders.Core.Entities;

namespace Orders.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
        }
    }
}