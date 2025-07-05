using AutoMapper;
using EmployeeListApplication.Core.Infrastructure;
using EmployeeListApplication.Core.Infrastructure.Repositories;
using EmployeeListApplication.Core.Infrastructure.Repositories.Interfaces;
using EmployeeListApplication.Core.Services;
using EmployeeListApplication.Core.Services.Interfaces;
using EmployeeListApplication.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();