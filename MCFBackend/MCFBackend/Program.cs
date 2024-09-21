using MCFBackend.Context;
using MCFBackend.Services.Helper;
using MCFBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

// Method to configure services
void ConfigureServices(IServiceCollection services)
{
    // Configure the DbContext
    services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContextConnection")));

    // Add authentication services
    services.AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
        {
            options.LoginPath = "/auth/login";
        });

    // Register the repositories
    services.AddScoped<ILogin, LoginRepository>();
    services.AddScoped<IBpkb, BpkbRepository>();

    // Configure CORS to allow frontend requests
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
                builder.WithOrigins("https://your-frontend-url.com") // Adjust to your frontend URL
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
    });

    // Add controllers
    services.AddControllers();

    // Add Swagger for API documentation
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

// Method to configure middleware
void ConfigureMiddleware(WebApplication app)
{
    // Use exception handling in non-development environments
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowSpecificOrigins"); // Enable CORS policy here
    app.UseAuthentication();
    app.UseAuthorization();

    // Map controllers to endpoints
    app.MapControllers();

    // Use Swagger in development
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
