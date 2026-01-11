using Management.Application.Interfaces;
using Management.Infrastructure.Brokers;
using Management.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStudentExcelBroker, StudentExcelBroker>();

        return services;
    }
}
