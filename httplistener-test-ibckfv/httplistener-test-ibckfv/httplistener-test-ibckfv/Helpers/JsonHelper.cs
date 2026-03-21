using System.Net;
using System.Text.Json;

namespace httplistener_test_ibckfv.Helpers;

public static class JsonHelper
{
    public static async Task<T?> ReadJsonBody<T>(HttpListenerRequest req)
    {
        using var ms = new MemoryStream();
        await req.InputStream.CopyToAsync(ms);
        ms.Position = 0;
        return JsonSerializer.Deserialize<T>(ms.ToArray(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
    public static async Task WriteJson(HttpListenerResponse res, object obj)
    {
        res.ContentType = "application/json";
        var bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
        res.ContentLength64 = bytes.Length;
        await res.OutputStream.WriteAsync(bytes, 0, bytes.Length);
        res.OutputStream.Close();
    }
}