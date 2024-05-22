using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Extensions;
using TabTabGo.WebStream.Services.EventHandlers;
using TabTabGo.WebStream.SignalR.Hub;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSignalR();
builder.Services.AddScoped<NullReceiveEvent>();
builder.Services.AddWebStream(builder =>
{
    builder.RegisteEventHandler<NullReceiveEvent>();
    builder.SetupEventHandlers(eventHandler =>
    {
        eventHandler.UseAllPassedHandlers(complex => // use all handlers pass the conditions
        {
            complex.AddEventHandler(s => s.EventName.StartsWith("event1"), (s) => s.UseFirstPassHandler(x => // use first event handler pass the condition
            {
                x.AddEventHandler("event1_sub1", x1 => x1.IgnoreAllEvents())
                .AddEventHandler("event1_sub2", x1 => x1.IgnoreAllEvents());
            }
            ))
            .AddEventHandler(s => s.EventName.StartsWith("event2"), s => s.UseEventHandlers((s) =>//use all events handlers
            {
                s.AddEventHandler(x => x.IgnoreAllEvents())
                .AddEventHandler((x) => x.UseEventHandler<NullReceiveEvent>()) // add handler implemented by library client 
                .AddEventHandler(x => x.IgnoreAllEvents());
            }));

        });
    });
    builder.SetupIPushEvent(s =>
          s.AddSignalR() 
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.MapHub<TabtabGoHub>("TabtabgoHub");
app.Run();
