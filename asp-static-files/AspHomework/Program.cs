using AspHomework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<InventoryAddItemMiddleware>();
builder.Services.AddScoped<ViewInventoryMiddleware>();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSession();
app.UseMiddleware<InventoryAddItemMiddleware>();
app.UseMiddleware<ViewInventoryMiddleware>();

app.Run();