using Microsoft.AspNetCore.SignalR.Client; 


var builder = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder();
builder.WithUrl("wss://localhost:7297/WebStreamHub");
builder.WithAutomaticReconnect();
var connection = builder.Build();

connection.StartAsync().Wait();// this will Invoke OnConnectedAsync in tabtabgoHub

await connection.InvokeAsync("ClientEvent", new  { EventName = "event1_sub1", Data =new { }  });
connection.On<string>("HandShake", s =>
{
    Console.Write(s.ToString());
});
while (true)
{
Console.ReadLine();

}

