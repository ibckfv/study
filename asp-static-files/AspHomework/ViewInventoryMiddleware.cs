using System.Text.Json;

namespace AspHomework;

public class ViewInventoryMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path.StartsWithSegments("/inventory/view")
            && context.Request.Method == HttpMethods.Get)
        {
            var inventoryJson = context.Session.GetString("MinecraftInventory");
            var items = string.IsNullOrEmpty(inventoryJson) 
                ? new List<MinecraftItem>() 
                : JsonSerializer.Deserialize<List<MinecraftItem>>(inventoryJson) ?? new List<MinecraftItem>();
            
            var category = context.Request.Query["category"].FirstOrDefault();
            var minCount = context.Request.Query["minCount"].FirstOrDefault();

            var filteredItems = items!.AsEnumerable();
                
            if (!string.IsNullOrEmpty(category))
            {
                filteredItems = filteredItems.Where(i => i.Category?.Equals(category, StringComparison.OrdinalIgnoreCase) == true);
            }
                
            if (int.TryParse(minCount, out int min))
            {
                filteredItems = filteredItems.Where(i => i.Count >= min);
            }

            var itemList = filteredItems.ToList();

            context.Items["Inventory"] = itemList;
            context.Items["InventoryCount"] = itemList.Count;
            context.Items["TotalItems"] = items!.Count;

            if (context.Request.Path.StartsWithSegments("/api") ||
                context.Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = true,
                    totalItems = items.Count,
                    filteredCount = itemList.Count,
                    items = itemList,
                    categories = items.Select(i => i.Category).Distinct().ToList(),
                    stats = new
                    {
                        uniqueItems = items.Select(i => i.Name).Distinct().Count(),
                        totalCount = items.Sum(i => i.Count),
                        mostCommonCategory = items.GroupBy(i => i.Category)
                            .OrderByDescending(g => g.Count())
                            .FirstOrDefault()?.Key ?? "Нет"
                    }
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
        }
        
        await next(context);
    }
}