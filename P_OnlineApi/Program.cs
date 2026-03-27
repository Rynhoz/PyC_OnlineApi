using Microsoft.EntityFrameworkCore;
using P_OnlineApi.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// crear otro                                              
builder.Services.AddDbContext<PedidoDbContext>(options =>

    options.UseSqlite("Data Source=pedidos.db")
);

////Para cliente
//builder.Services.AddDbContext<PedidoDbContext>(options => options.UseSqlite("Data Source=clientes.db"));

////para pedidos
//builder.Services.AddDbContext<PedidoDbContext>(options => options.UseSqlite("Data Source=productos.db"));

////para detalles
//builder.Services.AddDbContext<PedidoDbContext>(options => options.UseSqlite("Data Source=detallePedidos.db"));


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
