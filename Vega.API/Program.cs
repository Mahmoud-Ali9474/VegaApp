using Vega.API.Data;
using Vega.API.Core;

using Microsoft.EntityFrameworkCore;
using Vega.API.Core.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddDbContextPool<VegaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Make).Assembly);
builder.Services.AddCors();

builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehiclePhotoRepository, VehiclePhotoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    RequestPath = "/uploads"
});
app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
//app.UseAuthorization();

app.MapControllers();

app.Run();
