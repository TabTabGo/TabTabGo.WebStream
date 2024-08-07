using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Extensions;
using TabTabGo.WebStream.MessageStorage.Builders;
using TabTabGo.WebStream.Notification.API.APIs;
using TabTabGo.WebStream.Notification.EFCore;
using TabTabGo.WebStream.Services.EventHandlersServices;
using TabTabGo.WebStream.SignalR.Extensions.Builders;
using TabTabGo.WebStream.SignalR.Hub;
using TabTabGo.WebStream.TestApp;
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

/*
 * please setup the UnitOfWork of TabTabGo.Core Here
*/
builder.Services.AddDbContext<DbContext, NotificationDbContext>(s => s.UseMySql("Server=127.0.0.1;Database=WebStream;Uid=root;Pwd=root;Allow User Variables=true", ServerVersion.AutoDetect("Server=127.0.0.1;Database=WebStream;Uid=root;Pwd=root;Allow User Variables=true")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSignalR(s => s.EnableDetailedErrors = true);
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
    builder.UseEFCore();
    builder.SetupIPushEvent(s => s.AddSignalR<Guid,Guid>().LogAllOutMessages());
    builder.SetupIConnectionManager(s => s.AddConnectionToStorage().AddSignalR<Guid, Guid>());
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapNotificationsEndPoints<Guid, Guid>("tabtabgo");
app.MapHub<WebStreamHub<Guid, Guid>>("/WebStreamHub");
var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
dbContext.Database.EnsureCreated();
dbContext.Dispose();
scope.Dispose();
//test broadcast api
app.MapPost("broadcast", async (string message, IHubContext<WebStreamHub<Guid, Guid>> hubContext) =>
{
    await hubContext.Clients.All.SendAsync(message);
    return Results.NoContent;
});
app.Run();
