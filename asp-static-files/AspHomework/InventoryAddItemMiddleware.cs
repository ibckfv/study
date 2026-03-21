using System.Text.Json;

namespace AspHomework;

public class InventoryAddItemMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path.StartsWithSegments("/inventory/add") && 
            context.Request.Method == HttpMethods.Post)
        {
            if (context.Request.HasJsonContentType())
            {
                try
                {
                    var item = await context.Request.ReadFromJsonAsync<MinecraftItem>();

                    if (item != null)
                    {
                        var inventory = context.Session.GetString("MinecraftInventory");
                        var items = string.IsNullOrWhiteSpace(inventory)
                            ? new List<MinecraftItem>()
                            : JsonSerializer.Deserialize<List<MinecraftItem>>(inventory);
                
                        item.Id = items!.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
                
                        items.Add(item);
                
                        context.Session.SetString("MinecraftInventory", JsonSerializer.Serialize(items));

                        Console.WriteLine($"Предмет {item.Name} добавлен");
                
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                
                        var response = new
                        {
                            success = true,
                            message = $"Предмет '{item.Name}' добавлен в инвентарь",
                            item = item,
                            totalItems = items.Count
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                        
                    var errorResponse = new
                    {
                        success = false,
                        message = "Ошибка при добавлении предмета",
                        error = ex.Message
                    };
                        
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    return;
                }
            }
        }
        await next(context);
    }
}