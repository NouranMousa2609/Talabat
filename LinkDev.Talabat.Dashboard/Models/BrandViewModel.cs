using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.Models
{
	public class BrandViewModel
	{
			[Required(ErrorMessage = "Please enter a name.")]
			public string Name { get; set; }
		
	}
}
