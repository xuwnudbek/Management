using Management.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<StudentService>();
        services.AddScoped<ExcelService>();

        return services;
    }
}
