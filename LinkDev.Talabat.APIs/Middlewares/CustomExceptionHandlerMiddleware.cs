using Azure.Core;
using LinkDev.Talabat.APIs.Contollers.Exceptions;
using LinkDev.Talabat.APIs.Controllers.Errors;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Middlewares
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
		private readonly IWebHostEnvironment _environment;

		public CustomExceptionHandlerMiddleware(RequestDelegate next,ILogger<CustomExceptionHandlerMiddleware> logger,IWebHostEnvironment environment)
		{
			_next = next;
			_logger = logger;
			_environment = environment;
		}


		public async Task InvokeAsync (HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);

				//if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
				//{
				//	var response = new ApiResponse((int)HttpStatusCode.NotFound, $"the requested endpoint:{httpContext.Response} is not found");
				//	await httpContext.Response.WriteAsync(response.ToString());
				//}

			}
			catch (Exception ex)
			{
				ApiResponse response;
				switch (ex)
				{
					case NotFoundException:
						httpContext.Response.StatusCode=(int)HttpStatusCode.NotFound;
						httpContext.Response.ContentType = "application/json";
						 response = new ApiResponse(404, ex.Message);
						await httpContext.Response.WriteAsync(response.ToString());
						break;

					default:
				if (_environment.IsDevelopment())
				{
					//DevelpopmentMode
					_logger.LogError(ex, ex.Message);
					response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
				}
				else
				{
					//Production Miode
					//log exception in database
					response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				}
					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";

					await httpContext.Response.WriteAsync(response.ToString());
						break;
				}
			}
		}
	}
}
