using Microsoft.Extensions.DependencyInjection;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Application.Services;

namespace TheSketch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
