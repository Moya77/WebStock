using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http;
using WebStock.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICommandRegProduct, CommandRegProduct>();
builder.Services.AddScoped<IQueryGetInfoLote, QueryGetInfoLote>();
builder.Services.AddScoped<ICommandRegSalidaProducto, CommandRegSalidaProducto>();
builder.Services.AddScoped<IQueryGetFaltantes, QueryGetFaltantes>();
builder.Services.AddScoped<IQueryGetProductos, QueryGetProductos>();
builder.Services.AddScoped<IQueryGetProvedores, QueryGetProvedores>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Configuracion e inyeccion de IConfiguration

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

IConfiguration configuracion = configBuilder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
