using NegoAPI.Services.ProductService;
using NegoAPI.Services.SupplierService;
using NegoAPI.Services.TypeService;
using NegoAPI.Services.CustomerService;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Data;
using DotNetEnv;
using NegoAPI.Services.AlcoholProductService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//prendre exemple sur NegoSoftWeb/Program.cs pour la configuration de la base de données
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<NegoSoftContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAlcoholProductService, AlcoholProductService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
