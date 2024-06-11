using Microsoft.Extensions.FileProviders;
using Serilog;
using VotingSystem.Domain.Entities;
using VotingSystem.Infrastructure;
using VotingSystem.Application;
using Microsoft.AspNetCore.Identity;
using VotingSystem.Infrastructure.Data.Context;
using VotingSystem.Infrastructure.Identity.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

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

    #region Identity
    builder.Services.AddIdentity<PlatFormUser, PlatFormRole>(options =>
    {
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 5;
        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    })
        .AddEntityFrameworkStores<VotingSystemContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration.GetValue<string>("SecretKey");
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey!);
        var signingKey = new SymmetricSecurityKey(secretKeyInBytes);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    #endregion

    #region Localization

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
         var supportedCulture = new List<CultureInfo>
                {
                   new CultureInfo("en-Us"),
                   new CultureInfo("ar-Sa")
                };
        options.DefaultRequestCulture = new RequestCulture(culture: "ar", uiCulture: "Sa");
        options.SupportedCultures = supportedCulture;
        options.SupportedUICultures = supportedCulture;

    });

    builder.Services.AddLocalization();

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
    app.UseRequestLocalization();
    app.UseHttpsRedirection();
    app.UseAuthentication();
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
