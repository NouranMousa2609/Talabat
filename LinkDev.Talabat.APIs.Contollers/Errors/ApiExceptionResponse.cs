using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Errors
{
	public class ApiExceptionResponse:ApiResponse
	{
		public string? Details { get; set; }
		public ApiExceptionResponse(int StatusCode, string? Message = null,string? Details =null) : base(StatusCode, Message)
		{
			this.Details = Details;
		}

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }


    }

}
