using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Client.Models;
using Newtonsoft.Json;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5200/");

HttpResponseMessage responseMessage = await client.GetAsync("ticketlist");
string message = await responseMessage.Content.ReadAsStringAsync();

//TicketFlight
List<TicketFlight> ticketFlights = JsonConvert.DeserializeObject<List<TicketFlight>>(message)!;

HttpResponseMessage getTicketList = await client.GetAsync("ticketlist/370");
TicketFlight ticketFlight = JsonConvert.DeserializeObject<TicketFlight>(
    await getTicketList.Content.ReadAsStringAsync()
);

//HttpResponseMessage deleteTicket = await client.DeleteAsync("ticketdelete/370");

//TicketUser
List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(message);

HttpResponseMessage getTicketsUser = await client.GetAsync("ticketUser/0005432000987");
Ticket ticket = JsonConvert.DeserializeObject<Ticket>(
    await getTicketsUser.Content.ReadAsStringAsync()
);
Console.WriteLine(await getTicketsUser.Content.ReadAsStringAsync());

Console.Write("\nВведите новое имя: ");
ticket.PassengerName = Console.ReadLine()!;

JsonContent contentUser = JsonContent.Create(ticket);
HttpResponseMessage updateTicketUser = await client.PutAsync("ticketUserUpdate", contentUser);

if (updateTicketUser.IsSuccessStatusCode)
{
    HttpResponseMessage getupdateTicketsUser = await client.GetAsync("ticketUser/0005432000987");
    Ticket updateTicket = JsonConvert.DeserializeObject<Ticket>(
        await getupdateTicketsUser.Content.ReadAsStringAsync()
    );

    Console.WriteLine("\n" + await getupdateTicketsUser.Content.ReadAsStringAsync());
}
else
    Console.WriteLine("Провал!!!");
//
// Ticket CreateTicket = new Ticket()
// { 
//     TicketNo = "123",
//     BookRef = "BF16DC",
//     PassengerId = "123",
//     PassengerName = "Filip Circorov",
//     ContactData = "123",
// };
// JsonContent contentCreate = JsonContent.Create(CreateTicket);
// HttpResponseMessage createTicketUser = await client.PostAsync("Createticket", contentCreate);


