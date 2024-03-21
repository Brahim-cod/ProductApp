using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;
using Repository.Models;
using Repository.Repository;
using Repository.Repository.EFRepository;
using Repository.UnitOfWork;

namespace Repository.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositoryLayer(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository<Product, int>, ProductRepository>();
        services.AddScoped<IRepository<Category, int>, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkImp>();
        services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("SqlSeverConnection"))
        );
        return services;
    }
}
