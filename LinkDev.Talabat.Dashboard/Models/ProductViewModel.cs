using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is Description!!!")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }

        public string? PictureUrl { get; set; }
        [Required(ErrorMessage = "Price is Required!!!")]

        [Range(1,1000)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Category Id is Required!!!")]

        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        [Required(ErrorMessage = "Brand Id is Required!!!")]

        public int? BrandId { get; set; } 
        public ProductBrand? Brand { get; set; }

        


    }
}
