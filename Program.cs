using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data;
using RamsTrackerAPI.Mappings;
using RamsTrackerAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RamsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RamsTrackerConnectionString")));

builder.Services.AddScoped<IMSRepository, SQLMSRepository>();
builder.Services.AddScoped<IRaRepository, SQLRaRepository>();
builder.Services.AddScoped<ISubcontractorRepository, SQLSubcontractorRepository>();

builder.Services.AddAutoMapper(typeof(AutomapperProfiles));

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
