using AutoFixture;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Spring.Application.DependencyResolution;

namespace Spring.UnitTests;
public class BaseTest<T> where T : class
{
    protected Fixture Fixture;
    protected Random Random;
    protected IValidator<T> Validator;
    protected IServiceCollection Services;

    [OneTimeSetUp]
    public void Init()
    {
        Fixture = new();
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Random = new();

        var builder = WebApplication.CreateBuilder();

        Services = builder.Services;
        Services.RegisterApplicationDI();
        SetupServices();

        builder.Build();

        var serviceProvider = builder.Services.BuildServiceProvider();
        Validator = serviceProvider.GetRequiredService<IValidator<T>>();
    }

    [SetUp]
    protected virtual void SetupServices()
    {

    }
}
