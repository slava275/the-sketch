using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TheSketch.Application;
using TheSketch.Application.Validators;
using TheSketch.Infrastructure;
using TheSketch.Infrastructure.Context;
using TheSketch.WebApi.Middlewares;

namespace TheSketch.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddRouting(builder => 
                builder.LowercaseUrls = true);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddValidatorsFromAssemblyContaining<CreateArticleValidator>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TheSketchDbContext>();

                    context.Database.Migrate();

                    Console.WriteLine("--> Міграції успішно застосовані до бази даних.");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "--> Сталася помилка під час застосування міграцій.");

                    throw;
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
