using Microsoft.EntityFrameworkCore;
using SegurosLafiseBackend.Entities;
using SegurosLafiseBackend.Middleware;
using SegurosLafiseBackend.Repositories;
using SegurosLafiseBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SegurosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICoverageCategoryRepository, CoverageCategoryRepository>();
builder.Services.AddScoped<ICoverageCategoryService, CoverageCategoryService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ICoverageRepository, CoverageRepository>();
builder.Services.AddScoped<ICoverageService, CoverageService>();
builder.Services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();
builder.Services.AddScoped<IInsurancePolicyService, InsurancePolicyService>();

var app = builder.Build();

// Middleware de manejo de errores global
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
