using AutoMapper;
using UdemyCloneMicroservice.Basket.Api.Data;
using UdemyCloneMicroservice.Basket.Api.Dto;

namespace UdemyCloneMicroservice.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, BasketEntity>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}