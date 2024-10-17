using Azure;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly IWebHostEnvironment _environment;

		public ExceptionHandlerMiddleware(RequestDelegate next,ILogger<ExceptionHandlerMiddleware> logger,IWebHostEnvironment environment)
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
				if (_environment.IsDevelopment())
				{
					//DevelpopmentMode
					_logger.LogError(ex, ex.Message);
				}
				else
				{
					//Production Miode
					//log exception in database

				}
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			ApiResponse response;
			switch (ex)
			{
				case NotFoundException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(404, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				case BadRequestException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(400, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());
					break;

				default:
					response=_environment.IsDevelopment()?
							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
							:
							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";

					await httpContext.Response.WriteAsync(response.ToString());
					break;
			}
		}
	}
}
