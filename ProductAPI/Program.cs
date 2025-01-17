using Microsoft.AspNetCore.Identity;
using Repository.Extensions;
using Repository.Models;
using Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceLayer()
    .AddRepositoryLayer(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("blazorApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5216", "https://localhost:7137")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
    );
});

//builder.Services.AddAuthentication()
//    .AddBearerToken(IdentityConstants.BearerScheme);

//builder.Services.AddAuthorization();

//builder.Services.AddIdentityCore<AppUser>()
//    .AddApiEndpoints();


    
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

app.UseCors("blazorApp");

app.Run();
