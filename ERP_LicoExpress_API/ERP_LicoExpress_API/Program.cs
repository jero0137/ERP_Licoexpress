using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Repositories;
using ERP_LicoExpress_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PgsqlDbContext>();



builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


builder.Services.AddScoped<SupplierService>();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
