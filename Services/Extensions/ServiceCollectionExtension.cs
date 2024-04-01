using Microsoft.Extensions.DependencyInjection;
using Repository.Models;
using Services.Services;
using Shared.Services;
using Shered.Services;
using System.Reflection;

namespace Services.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderProductService, OrderProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
       
        return services;
    }
}
