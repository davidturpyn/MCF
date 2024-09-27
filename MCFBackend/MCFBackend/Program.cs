using MCFBackend.Context;
using MCFBackend.Services.Helper;
using MCFBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Access environment and set configuration files
var environment = builder.Environment;

builder.Configuration
    .SetBasePath(environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true) 
    .AddEnvironmentVariables();

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

// Method to configure services
void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
{
    // Configure DbContext with environment-specific connection string
    services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ApplicationContextConnection")));

    // Add authentication services
    services.AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
        {
            options.LoginPath = "/auth/login";
        });

    // Register the repositories
    services.AddScoped<ILogin, LoginRepository>();
    services.AddScoped<IBpkb, BpkbRepository>();

    // Configure CORS based on the environment
    if (environment.IsDevelopment())
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
    }
    else
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                builder.WithOrigins("https://your-production-frontend.com") // Adjust for production frontend
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });
    }

    // Add controllers
    services.AddControllers();

    // Add Swagger for API documentation only in development
    if (environment.IsDevelopment())
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}

// Method to configure middleware
void ConfigureMiddleware(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    // Enable CORS policy based on the environment
    if (app.Environment.IsDevelopment())
    {
        app.UseCors("AllowAllOrigins");
    }
    else
    {
        app.UseCors("AllowSpecificOrigins");
    }

    app.UseAuthentication();
    app.UseAuthorization();

    // Map controllers to endpoints
    app.MapControllers();

    // Use Swagger in development only
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
