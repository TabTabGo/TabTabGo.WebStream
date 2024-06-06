using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using TabTabGo.WebStream.Extensions;
using TabTabGo.WebStream.Notification.API.APIs;
using TabTabGo.WebStream.Notification.Builders;
using TabTabGo.WebStream.Notification.EFCore;
using TabTabGo.WebStream.Services.EventHandlersServices;
using TabTabGo.WebStream.SignalR.Extensions.Builders;
using TabTabGo.WebStream.SignalR.Hub;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddControllers();

builder.Services.AddSignalR();
builder.Services.DatabaseFactory(builder.Configuration);
builder.Services.UseUnitOfWork(builder.Configuration);

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

        })
        .LogAllRecevedMessages();
    });

    builder.UseEfCore();
    
    builder.SetupIPushEvent(s => s.AddSignalR().LogAllOutMessages());

    builder.SetupIConnectionManager(s => s.AddSignalR().AddConnectionToStorage());
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapNotificationsEndPoints("tabtabgo");
app.MapHub<WebStreamHub>("/WebStreamHub");
//test broadcast api
app.MapPost("broadcast", async (string message, IHubContext<WebStreamHub> hubContext) =>
{
    await hubContext.Clients.All.SendAsync(message);
    return Results.NoContent;
});
app.Run();
