var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "proveedores",
    pattern: "{controller=Proveedor}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "productos",
    pattern: "{controller=Proveedor}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "usuarios",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "cuenta",
    pattern: "{controller=Cuenta}/{action=Index}/{id?}");

app.Run();
