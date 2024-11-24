using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{

    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;
        public OrderItemPictureUrlResolver(IConfiguration configuration) => this.configuration = configuration;
        public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";
            return string.Empty;
        }
    }
}