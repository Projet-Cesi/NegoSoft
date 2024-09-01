using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Data;
using DotNetEnv;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Services.ProductService;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<NegoSoftContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NegoSoftContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, ProductService>();

//add session to the application
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // durée de la session
    options.Cookie.HttpOnly = true; // le cookie de session ne peut pas être accédé par le client
    options.Cookie.IsEssential = true; // le cookie de session est essentiel
});
builder.Services.AddHttpContextAccessor(); // pour accéder à la session dans les services

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
