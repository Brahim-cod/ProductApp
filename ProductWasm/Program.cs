using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductWasm;
using ProductWasm.Services;
using ProductWasm.Services.Abstract;
using Shared.Services;
using Blazored.Toast;
using Shered.Services;
using Microsoft.AspNetCore.Components.Authorization;
using ProductWasm.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5003/") });

builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore();

// DI
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();




await builder.Build().RunAsync();
