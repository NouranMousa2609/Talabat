using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application
{
	internal class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ReturnedProductDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(d=>d.Category ,O=>O.MapFrom(src=>src.Category!.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}
