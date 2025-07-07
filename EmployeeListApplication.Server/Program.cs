using System.Text;
using EmployeeListApplication.Core.Infrastructure;
using EmployeeListApplication.Core.Infrastructure.Interfaces;
using EmployeeListApplication.Core.Infrastructure.Repositories;
using EmployeeListApplication.Core.Infrastructure.Repositories.Interfaces;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services;
using EmployeeListApplication.Core.Services.Interfaces;
using EmployeeListApplication.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Hard coded secret values (only for quick projects)
const string secretKey = "YourSuperSecretKeyForDevelopment12345";
Environment.SetEnvironmentVariable("SECRET", secretKey);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// AddNewtonsoftJson are for json patch documents
builder.Services.AddControllers().AddNewtonsoftJson(); 

// Register Services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Register Repositories
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Add Entity Framework and use sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Auto DB Creation and Seeding
builder.Services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();

// Configure anything auth related
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };

    });

builder.Services.AddIdentityCore<User>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 10;
    o.User.RequireUniqueEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseDeveloperExceptionPage(); // This one is commented out so that it will not override the custom global exception handler
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

// This is for simplicity of this project
// In a production project CORS needs to be configured properly
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SEED DATABASE
using var scope = app.Services.CreateScope();
try
{
    var dbSeeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
    await dbSeeder.SeedAsync();
}
catch (Exception ex)
{;
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogCritical(ex, "An error occurred while seeding database");
    throw;
}

app.Run();