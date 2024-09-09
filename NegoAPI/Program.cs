using NegoAPI.Services.ProductService;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//prendre exemple sur NegoSoftWeb/Program.cs pour la configuration de la base de données
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<NegoSoftContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IProductService, ProductService>();

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
