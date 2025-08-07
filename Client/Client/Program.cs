HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7088/");

HttpResponseMessage responseMessage = await client.GetAsync("/");
string message = await responseMessage.Content.ReadAsStringAsync();
Console.WriteLine(message);