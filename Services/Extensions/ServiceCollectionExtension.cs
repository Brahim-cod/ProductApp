using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        return services;
    }
}
