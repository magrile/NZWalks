using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeccion de la dependencia de la BBDD

builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
});


// Inyectamos la dependencia de regiones

builder.Services.AddScoped<IRegionRepository, RegionRepository>();

// Inyectamos la dependencia de Walk

builder.Services.AddScoped<IWalkRepository, WalkRepository>();

// Inyectamos la dependencia de regiones

builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();

// iNYECTAMOS EL AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Ignoramos los ciclos
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

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
