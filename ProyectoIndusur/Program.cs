using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISQLServerIndusur, SQLServerIndusur>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehiculoServicio, VehiculoServicio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehiculo}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// en caso este la clase startup.cs

//public void ConfigureServices(IServiceCollection services)
//{
//    var connectionString = Configuration.GetConnectionString("DefaultConnection");

//    // Agregar el repositorio como servicio
//    services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));

//    // Otros servicios
//    services.AddControllersWithViews();
//}