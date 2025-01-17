using F9.Src.BusinessLogic;
using F9.Src.DataAccess;
using FACommon.Src.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace F9.Src;

public sealed class F9Register : IServiceRegister
{
    public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(F9Register).Assembly;

        #region Filters
        services.RegisterFiltersFromAssembly(currentAssembly);
        #endregion

        #region Validation
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Singleton);
        #endregion

        #region Core
        services
            .AddScoped<IF9Repository, F9Repository>()
            .MakeScopedLazy<IF9Repository>()
            .AddScoped<F9Service>();
        #endregion

        return services;
    }
}
