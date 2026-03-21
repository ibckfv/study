using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using httplistener_test_ibckfv.Database;
using httplistener_test_ibckfv.Dto;
using httplistener_test_ibckfv.Helpers;
using httplistener_test_ibckfv.Repositories;
using httplistener_test_ibckfv.Services;

class Program
{
    static async Task Main()
    {
        const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1469;SearchPath=oristest;";
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:7777/");
        listener.Start();

        var db = new Database(connectionString);
        var cityRepo = new CityRepository(db);
        var userRepo = new UserRepository(db);
        var aptRepo = new ApartmentRepository(db, cityRepo);
        var bookingRepo = new BookingRepository(db);
        var sessionStore = new SessionStore();

        var authService = new AuthController(userRepo, sessionStore);
        var aptService = new ApartmentController(aptRepo, cityRepo);
        var bookingService = new BookingController(bookingRepo, aptRepo);

        while (listener.IsListening)
        {
            var ctx = await listener.GetContextAsync();
            _ = Task.Run(async () =>
            {
                try
                {
                    await ApiRouter.Route(ctx, authService, aptService, bookingService);
                }
                catch (Exception ex)
                {
                    ctx.Response.StatusCode = 500;
                    await JsonHelper.WriteJson(ctx.Response, new { error = ex.Message });
                }
            });
        }
    }
}
