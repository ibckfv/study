using System.Collections.Concurrent;
using httplistener_test_ibckfv.models;

namespace httplistener_test_ibckfv.Services;

public class SessionStore
{
    private ConcurrentDictionary<string, Session> _store = new();
    public string Create(int userId)
    {
        var sid = Guid.NewGuid().ToString();
        _store[sid] = new Session { UserId = userId, ExpiresAt = DateTime.UtcNow.AddHours(8) };
        return sid;
    }
    public int? GetUserId(string sid)
    {
        if (sid == null) return null;
        if (_store.TryGetValue(sid, out var s))
        {
            if (s.ExpiresAt > DateTime.UtcNow) return s.UserId;
            _store.TryRemove(sid, out _);
        }
        return null;
    }
}