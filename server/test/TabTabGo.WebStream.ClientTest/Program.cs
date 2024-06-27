using Microsoft.AspNetCore.SignalR.Client; 


var builder = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder();
var token = Console.ReadLine();
builder.WithUrl("https://localhost:7008/WebStreamHub",s=>s.AccessTokenProvider=() =>Task.FromResult(token));
builder.WithAutomaticReconnect();
var connection = builder.Build();

connection.StartAsync().Wait();// this will Invoke OnConnectedAsync in tabtabgoHub
Console.WriteLine(connection.State.ToString()); 
connection.On<dynamic>("SendNotification", s =>
{
    Console.WriteLine(s.ToString());
});
while (true)
{
    await Task.Delay(25100000);

}

