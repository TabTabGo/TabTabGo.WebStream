using TabTabGo.WebStream.SignalR.Hub;
using TabTabGo.WebStream.Extensions;
using TabTabGo.WebStream.Builders;
using Services;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSignalR();
builder.Services.AddWebStream((s, builder) =>
{
    builder.UseEventHandler(eventHandler =>
    {
        eventHandler.UseComplexHandler(complex =>
        {
            complex
            .AddEventHandler(s => s.EventName.Equals("event1"), (s) => s.IgnoreAllEvents())
            .AddEventHandler(s => s.EventName.Equals("event2"), s => s.SetEventHandler(new NullReceiveEvent() /*also ignore */ ));

        });
    });
    builder.UseSignalR(s);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapHub<TabtabGoHub>("testHub");


app.MapHub<TabtabGoHub>("TabtabgoHub");
app.Run(); 
