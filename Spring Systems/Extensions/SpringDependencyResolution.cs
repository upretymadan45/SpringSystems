namespace Spring_Systems.Extensions;
public static class SpringDependencyResolution
{
    public static IServiceCollection RegisterSpringDI(this IServiceCollection services)
    {
        services.AddScoped<ISpringDbConnection, SpringDbConnection>()
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}
