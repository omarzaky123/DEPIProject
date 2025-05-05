using AutoMapper;
using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.Models;

namespace DEPIAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CatigoryName, opt => opt.MapFrom(src => src.Catigory.Name))
                .ForMember(dest => dest.CatigoryDescription, opt => opt.MapFrom(src => src.Catigory.Description))
                .ReverseMap();

            CreateMap<Catigory, CatigoryDTO>()
             .ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ReverseMap();
            CreateMap<Guset, GusetFullModelDTO>()
                  .ReverseMap();

            CreateMap<Catigory, CatigoryWithPrdDTO>()
             .ReverseMap();
            CreateMap<Product, ProductInsertDTO>()
                    .ReverseMap();
            CreateMap<Vartion, VartionInsertDTO>()
                 .ReverseMap();
            CreateMap<Vartion, VartionWithCatDTO>()
                .ReverseMap();

            CreateMap<ShoppingCart, ShoppinCartDTO>()
             .ReverseMap();
            CreateMap<CartItem, CartItemDTO>()
                .ReverseMap();
            CreateMap<ProductVartion, ProductVartionDTO>()
                .ReverseMap();
            CreateMap<Guset, GusetDTO>()
                .ReverseMap();
            CreateMap<Order, OrderDTO>()
                .ReverseMap();
            CreateMap<Order, OrderDTOInsert>()
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ReverseMap();
            CreateMap<OrderItem, OrderItemInsert>()
                .ReverseMap();
            CreateMap<ProductVartion, ProductVartionDTOInsert>()
                .ReverseMap();
            CreateMap<ProductImage, ProductImageInsertDTO>()
                    .ReverseMap();
            //CreateMap<ProductImage, ProductImageDTO>()
            //        .ReverseMap();

        }
    }
    }

