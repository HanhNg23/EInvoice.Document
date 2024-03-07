
using EInvoice.Document.Infrastructure;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EInvoice.Document.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // The initialized WebApplicationBuilder (builder) provides default
            // configuration and calls AddUserSecrets when the EnvironmentName
            // is Development:
            var builder = WebApplication.CreateBuilder(args); // initialize a new instance of WebApplicationBuilder with preconfigured defaults

            // Add services to the container.  
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
