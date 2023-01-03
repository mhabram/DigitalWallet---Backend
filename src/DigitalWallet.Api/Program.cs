using DigitalWallet.Api;
using DigitalWallet.Application;
using DigitalWallet.Infrastructure;
using DigitalWallet.Infrastructure.Persistence.Initlialisers;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddApiServices();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        using (var scope = app.Services.CreateScope())
        {
            var identityInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContextInitialiser>();
            await identityInitialiser.InitialiseAsync();

            var businessInitialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await businessInitialiser.InitialiseAsync();
        }
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
