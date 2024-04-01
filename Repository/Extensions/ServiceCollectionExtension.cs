using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Models;
using Repository.Repository;
using Repository.Repository.EFRepository;
using Repository.UnitOfWork;
using System.Text;

namespace Repository.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositoryLayer(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository<Product, int>, ProductRepository>();
        services.AddScoped<IRepository<Category, int>, CategoryRepository>();
        services.AddScoped<IRepository<Order, int>, OrderRepository>();
        services.AddScoped<IRepository<OrderProduct, (int, int)>, OrderProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkImp>();
        

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlSeverConnection")),
            ServiceLifetime.Scoped
        );


        //Auth Config Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddRoles<IdentityRole>();


        //Jwt

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        return services;
    }
}
