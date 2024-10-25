using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		//[FromServices]
		//public static StoreContext StoreContext { get; set; } = null!;
		public static async Task Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services



			// Add services to the container.

			builder.Services
				.AddControllers()
                .AddApplicationPart(typeof(Contollers.AssemblyInformation).Assembly)
                .ConfigureApiBehaviorOptions(options =>
				    { 
					options.SuppressModelStateInvalidFilter=false;
						options.InvalidModelStateResponseFactory = (actionContext) =>
						{
							var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
												   .SelectMany(p => p.Value!.Errors)
												   .Select(e => e.ErrorMessage);
												   //.Select(p => new ApiValidationErrorResponse.ValidationError()
												   //{
													  // Field = p.Key,
													  // Errors = p.Value!.Errors.Select(E=>E.ErrorMessage)
												   //});
							return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
						};
                    });

			///builder.Services.Configure<ApiBehaviorOptions>(options =>
			///{
			///	options.SuppressModelStateInvalidFilter = false;
			///	options.InvalidModelStateResponseFactory = (actionContext) =>
			///	{
			///		var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
			///							   .SelectMany(p => p.Value!.Errors)
			///							   .Select(E => E.ErrorMessage);
			///		return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
			///	};
			///});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddScoped(typeof(ILoggedInUserService),typeof(LoggedInUserService));
			//builder.Services.AddPersistenceService(builder.Configuration);
			//DependencyInjection.AddPersistenceService(builder.Services,builder.Configuration);
			builder.Services.AddPersistenceService(builder.Configuration);
			builder.Services.AddApplicationService();
			builder.Services.AddInfrastructureServices(builder.Configuration);

			builder.Services.AddIdentityService(builder.Configuration);
			#endregion

			var app = builder.Build();


			#region Databases Initialization 

			await app.InitializeStoreContextAsync();

			#endregion

			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionHandlerMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}");

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseStaticFiles();
			app.MapControllers();

			app.Run(); 
			#endregion
		}
	}
}
