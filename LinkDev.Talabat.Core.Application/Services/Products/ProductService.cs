﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
namespace LinkDev.Talabat.Core.Application.Services.Products
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ReturnedProductDto>> GetProductsAsync()
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var MappedProducts = mapper.Map<IEnumerable<ReturnedProductDto>>(Products);

            return MappedProducts;
        }


        public async Task<ReturnedProductDto> GetProductAsync(int id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var MappedProduct = mapper.Map<ReturnedProductDto>(Product);
            return MappedProduct;
        }



        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var mappedBrands = mapper.Map<IEnumerable<BrandDto>>(brands);
            return mappedBrands;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            var mappedcategories = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return mappedcategories;
        }
    }
}