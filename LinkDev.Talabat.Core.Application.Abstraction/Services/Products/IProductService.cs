using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Products
{
    public interface IProductService
    {

        Task<IEnumerable<ReturnedProductDto>> GetProductsAsync(string? sort);


        Task<ReturnedProductDto> GetProductAsync(int id);



        Task<IEnumerable<BrandDto>> GetBrandsAsync();

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();



    }
}
