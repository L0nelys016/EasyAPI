using EasyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PostgresContext>();
WebApplication app = builder.Build();

app.MapGet("/", () => "EasyAPI");

app.MapGet(
    "/ticketlist",
    (PostgresContext db) =>
        JsonConvert.SerializeObject(
            db.TicketFlights.Include(x => x.TicketNoNavigation).ToList().Take(10),
            Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
        )
);

app.MapGet(
    "/ticketlist/{_flightId:int}",
    (PostgresContext db, int _flightId) =>
        JsonConvert.SerializeObject(
            db.TicketFlights.Include(x => x.TicketNoNavigation)
                .FirstOrDefault(x => x.FlightId == _flightId),
            Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
        )
);

app.MapGet(
    "/ticketUser/{ticket_no}",
    (PostgresContext db, string ticket_no) =>
        JsonConvert.SerializeObject(
            db.Tickets.FirstOrDefault(x => x.TicketNo == ticket_no),
            Formatting.Indented,
            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
        )
);

app.MapDelete(
    "/ticketdelete/{_flightId:int}",
    (PostgresContext db, int _flightId) =>
    {
        db.TicketFlights.Remove(db.TicketFlights.FirstOrDefault(x => x.FlightId == _flightId));
        db.SaveChanges();
    }
);

app.MapPut(
    "/ticketUserUpdate",
    (PostgresContext db, Ticket _ticket) =>
    {
        db.Tickets.Update(_ticket);
        db.SaveChanges();
    }
);

app.Run();
