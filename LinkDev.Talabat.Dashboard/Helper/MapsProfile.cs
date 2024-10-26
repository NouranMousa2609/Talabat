using AutoMapper;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Models;

namespace LinkDev.Talabat.Dashboard.Helper
{
    public class MapsProfile:Profile
    {
        public MapsProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }




    }
}
