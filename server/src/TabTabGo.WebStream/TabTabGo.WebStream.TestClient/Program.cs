// using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;


var builder = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder();
builder.WithUrl("wss://localhost:7297/TabtabgoHub", options =>
{
}); 

builder.WithAutomaticReconnect();
var connection = builder.Build();

connection.StartAsync().Wait();// this will invoice OnConnectedAsync in tabtabgoHub

connection.InvokeAsync("ClientEvent", new { EventName = "event1_sub1", Data = new { } });

Console.ReadLine();
