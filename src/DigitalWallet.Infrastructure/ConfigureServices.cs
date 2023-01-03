using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DigitalWallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Infrastructure.Common.Services;
using DigitalWallet.Infrastructure.Identity;
using DigitalWallet.Infrastructure.Persistence.Interceptors;
using DigitalWallet.Infrastructure.Identity.Settings;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DigitalWallet.Infrastructure.Common.Authentication;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Infrastructure.Persistence.Repositories.Queries;
using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;
using DigitalWallet.Infrastructure.Persistence.Repositories.Commands;
using DigitalWallet.Infrastructure.Persistence.Initlialisers;

namespace DigitalWallet.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Database context settings
        DbConnectionStringBuilder connectionStringBuilder = new()
        {
            { "Server", configuration["SQ_DB_HOST"]! },
            { "Database", configuration["SQ_DB_NAME"]! },
            { "Port", configuration["SQ_DB_PORT"]! },
            { "Username", configuration["SQ_DB_USER"]! },
            { "Password", configuration["SQ_DB_PASSWORD"]! },
            { "Keepalive", configuration["SQ_DB_KEEPALIVE"]! }
        };

        services.AddApplicationDbContext(configuration, connectionStringBuilder);
        services.AddIdentityDbContext(configuration, connectionStringBuilder);
        services.AddAuth(configuration);
        #endregion

        #region Services
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        
        services.AddScoped<ApplicationIdentityDbContextInitialiser>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        #endregion

        #region Queries
        services.AddScoped<IPersonQueriesRepository, PersonQueriesRepository>();
        services.AddScoped<IUserQueriesRepository, UserQueriesRepository>();
        #endregion

        #region Commands
        services.AddScoped<IPersonCommandsRepository, PersonCommandsRepository>();
        services.AddScoped<IUserCommandsRepository, UserCommandsRepository>();
        #endregion

        return services;
    }

    public static IServiceCollection AddIdentityDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        DbConnectionStringBuilder connectionStringBuilder)
    {
        if (!string.IsNullOrWhiteSpace(connectionStringBuilder.ConnectionString.FirstOrDefault().ToString()))
        {
            services.AddDbContext<ApplicationIdentityDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!,
                options => options.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName)));
        }
        else
        {
            services.AddDbContext<ApplicationIdentityDbContext>(
                options => options.UseNpgsql(connectionStringBuilder.ConnectionString,
                options => options.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName)));
        }

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            options.User.RequireUniqueEmail = true;
        }).AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        return services;
    }

    public static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        DbConnectionStringBuilder connectionStringBuilder)
    {
        if (!string.IsNullOrWhiteSpace(connectionStringBuilder.ConnectionString.FirstOrDefault().ToString()))
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!,
                options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(connectionStringBuilder.ConnectionString,
                options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}