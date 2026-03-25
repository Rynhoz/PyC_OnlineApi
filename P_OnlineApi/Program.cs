using Microsoft.EntityFrameworkCore;
using P_OnlineApi.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// crear otro                                              
builder.Services.AddDbContext<PedidoDbContext>(options =>
    ///Aqui va a ir a donde se va a conectar a la base de datos
    ///si es que es web enotnces habra una conexion
    options.UseSqlite("Data Source=pedidos.db")
);

//Para cliente
builder.Services.AddDbContext<PedidoDbContext>(options => options.UseSqlite("Data Source=clientes.db"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
