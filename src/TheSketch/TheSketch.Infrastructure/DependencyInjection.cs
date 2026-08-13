using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Application.Interfaces.Services.External;
using TheSketch.Domain.Common;
using TheSketch.Infrastructure.Context;
using TheSketch.Infrastructure.Repositories;
using TheSketch.Infrastructure.Services;

namespace TheSketch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TheSketchDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.Configure<CloudinarySettings>(options =>
        {
            var section = configuration.GetSection(CloudinarySettings.SectionName);
            options.CloudName = section["CloudName"]!;
            options.ApiKey = section["ApiKey"]!;
            options.ApiSecret = section["ApiSecret"]!;
        });
        services.AddScoped<IImageUploadService, CloudinaryImageUploadService>();

        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
