using Microsoft.OpenApi.Models;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.NotificationStorage.API.APIs;
using TabTabGo.WebStream.NotificationStorage.EFCore;
using TabTabGo.WebStream.NotificationStorage.Extensions;
using TabTabGo.WebStream.Services.EventHandlers;
using TabTabGo.WebStream.SignalR.Hub;
using TabTabGo.WebStream.MessageStorage.Builders;
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
//builder.Services.AddWebStream(bu => { });// to just add webStream services
builder.Services.AddNotificationServices(NotificationBuilder => // to add notification services and you can setup webstream too
{
    NotificationBuilder.UseEfCoreNotificationStorage();// this to store notification in database
    NotificationBuilder.SetUpWebStream(builder =>
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
        builder.UseEfCoreMessageStorage();// this to store messages in database
        builder.SetupIPushEvent(s => s.AddSignalR().AddPushToStorage().LogAllOutMessages());
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapTabtabGoNotificationsEndPoints("tabtabgo");
app.MapHub<TabtabGoHub>("TabtabgoHub");
app.Run();
