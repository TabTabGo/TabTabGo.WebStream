using Microsoft.AspNetCore.SignalR.Client; 


var builder = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder();
builder.WithUrl("wss://localhost:7297/TabtabgoHub");
builder.WithAutomaticReconnect();
var connection = builder.Build();

connection.StartAsync().Wait();// this will Invoke OnConnectedAsync in tabtabgoHub

connection.InvokeAsync("ClientEvent", new  { EventName = "event1_sub1", Data =new { }  });

Console.ReadLine();

