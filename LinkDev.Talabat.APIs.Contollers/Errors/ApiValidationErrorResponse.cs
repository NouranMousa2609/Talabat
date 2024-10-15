using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
	public class ApiValidationErrorResponse : ApiResponse
	{
		public required IEnumerable<string> Errors { get; set; }
		public ApiValidationErrorResponse( string? Message = null) : base(400, Message)
		{

		}
	}
}
