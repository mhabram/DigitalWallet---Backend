using DigitalWallet.Api.Common.Errors;
using DigitalWallet.Api.Common.Services;
using DigitalWallet.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DigitalWallet.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<ProblemDetailsFactory, DigitalWalletProblemDetailsFactory>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks();
        services.AddControllers();
        //services.AddControllersWithViews(options =>
        //    options.Filters.Add<ApiExceptionFilterAttribute>())
        //        .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // services.AddRazorPages();

        // Customize default API behaviour
        //services.Configure<ApiBehaviorOptions>(options =>
        //    options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
