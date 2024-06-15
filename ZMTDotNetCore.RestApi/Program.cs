using Microsoft.EntityFrameworkCore;
using ZMTDotNetCore.RestApi.Db;
using ZMTDotNetCore.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddScoped(n=>new AdoDotNetService(connectionString!));
builder.Services.AddScoped(n=>new DrapperService(connectionString!));
builder.Services.AddDbContext<AddDbContent>(opt =>
{
    opt.UseSqlServer(connectionString);
},ServiceLifetime.Transient,ServiceLifetime.Transient);
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
