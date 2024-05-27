using Microsoft.Extensions.FileProviders;
using Serilog;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure;
using VotingSystem.Application;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    #region Default Services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    #endregion

    #region Serilog 

    builder.Host.UseSerilog();

    #endregion

    #region Global Services
    builder.Services.AddInfraStructureConfiguration(builder.Configuration);
    builder.Services.AddApplicationConfiguration(builder.Configuration);
    #endregion

    #region Exception Handler
    // builder.Services.AddExceptionHandler<ExceptionHandler>();
    builder.Services.AddProblemDetails();
    #endregion

    #region CORS Policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllDomains", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
    #endregion

    #region Middlewares
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }



    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors("AllowAllDomains");

    app.MapControllers();

    app.Run();
    #endregion
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
