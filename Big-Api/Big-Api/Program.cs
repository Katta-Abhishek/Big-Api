using Big_Api.Data;
using Big_Api.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
 options.AddPolicy("AllowAllOrigins",
 builder =>
 {
   builder.AllowAnyHeader()
          .AllowAnyOrigin()
          .AllowAnyMethod();
 }));


ConfigurationManager configuration = builder.Configuration;


builder.Services.AddDbContext<ApplicationDbContext>( options => 
  options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
  );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
  AppEntityDataSeeder seeder = new(context);
  seeder.Seed(100); // Number of random entities to generate
}

app.Run();
