using Microsoft.EntityFrameworkCore;
using Fire.Infra.Data;
using Fire.Infra.Repositories;
using Fire.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql("PostgreSQL") // ou UseSqlServer, UseSqlite, etc.
//// , ServiceLifetime.Scoped) // 'Scoped' é o padrão e o correto para web.
//);

builder.Services.AddInfrasctructure(builder.Configuration);
//builder.Services.AddApplication(builder.Configuration);

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
