using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Spring.Application.DependencyResolution;
public static class ApplicationDIConfiguration
{
    public static IServiceCollection RegisterApplicationDI(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(conf=> {
            conf.DisableDataAnnotationsValidation = true;
            })
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(typeof(ApplicationDIConfiguration).Assembly);

        return services;
    }
}
