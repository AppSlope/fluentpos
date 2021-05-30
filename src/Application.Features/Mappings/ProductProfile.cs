using AutoMapper;
using FluentPOS.Domain.Entities;
using FluentPOS.Shared.ViewModels.Products;

namespace FluentPOS.Application.Features.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}