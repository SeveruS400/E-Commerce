using e_commerce.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Implementations;
using Services.Contracts;
using Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

#region Session
builder.Services.AddDistributedMemoryCache(); // Add in-memory caching for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout süresi
    options.Cookie.Name = "E_Commerce.Session";
    options.Cookie.HttpOnly = true; // Güvenlik amacýyla, client-side JavaScript ile eriþimi kapatýr
    options.Cookie.IsEssential = true; // GDPR uyumluluðu için
});
builder.Services.AddHttpContextAccessor();

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region DbContext
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DataConnection"));
    options.EnableSensitiveDataLogging(true);
}).AddScoped<DataContext>();
#endregion

#region Scoped
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IServiceManager,ServiceManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
#endregion

builder.Services.AddScoped<Cart>(c => SessionCart.GetCart(c));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});


app.Run();
