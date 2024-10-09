using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkDev.Talabat.Core.Application;
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

			builder.Services.AddControllers()
				.AddApplicationPart(typeof(Contollers.AssemblyInformation).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddScoped(typeof(ILoggedInUserService),typeof(LoggedInUserService));
			//builder.Services.AddPersistenceService(builder.Configuration);
			DependencyInjection.AddPersistenceService(builder.Services,builder.Configuration);
			builder.Services.AddApplicationService();
			#endregion

			var app = builder.Build();


			#region Databases Initialization 

			await app.InitializeStoreContextAsync(); 

			#endregion

			#region Configure Kestrel Middlewares

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run(); 
			#endregion
		}
	}
}
