using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

			//builder.Services.AddPersistenceService(builder.Configuration);
			DependencyInjection.AddPersistenceService(builder.Services,builder.Configuration);
			#endregion

			var app = builder.Build();

			var Scope=app.Services.CreateScope();
			var service=Scope.ServiceProvider;
			var dbContext=service.GetRequiredService<StoreContext>();

			var loggerFactory=service.GetRequiredService<ILoggerFactory>();
			try
			{
				var PendingMigrations = dbContext.Database.GetPendingMigrations();

				if (PendingMigrations.Any())
				    await dbContext.Database.MigrateAsync();

			}
			catch(Exception ex) 
			{

				var logger=loggerFactory.CreateLogger<Program>();

				logger.LogError (ex, "An error has occured");

			}
			
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
