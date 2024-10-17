using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string ProductName { get; set; }

        public string? PictureUrl { get; set; }

        [Required]
        [Range(.1, double.MaxValue, ErrorMessage = "Price Must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be greater than one")]
        public int Quantity { get; set; }

        public string? Brand { get; set; }
        public string? Category { get; set; }

    }
}
