using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using TabTabGo.Core.Data;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Notification.DTOs;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;

namespace TabTabGo.WebStream.Notification.API.APIs
{
    public static class Extension
    {
        public static IEndpointRouteBuilder MapNotificationsEndPoints<TUserKey, TTenantKey>(
            this IEndpointRouteBuilder endpointRouteBuilder)
            where TUserKey : struct where TTenantKey : struct
        {
            const string tag = "Notification";
            endpointRouteBuilder.MapPost("notifications/read/all", async (
                    [FromServices] INotificationUserRepository repo,
                    [FromServices] IUnitOfWork unitOfWork,
                    [FromServices] INotificationServices<string> service,
                    [FromServices] ISecurityService<TUserKey, TTenantKey> securityService,
                    CancellationToken cancellationToken) =>
                {
                    var userId = securityService?.GetUserId().ToString();
                    if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                    unitOfWork.BeginTransaction();

                    await service.ReadAllNotifications(userId, cancellationToken);
                    unitOfWork.Commit();
                    return Results.Ok();
                })
                .WithSummary($"Read all notifications")
                .WithDescription($"# set notifications as read")
                .WithName($"ReadAllNotificationMessages")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();

            endpointRouteBuilder.MapPost("notifications/{notificationMessageId}/read", async (
                    [FromServices] INotificationUserRepository repo,
                    [FromServices] IUnitOfWork unitOfWork,
                    [FromServices] INotificationServices<string> service,
                    [FromServices] ISecurityService<TUserKey, TTenantKey> securityService,
                    HttpRequest request,
                    Guid notificationMessageId,
                    CancellationToken cancellationToken) =>
                {
                    var userId = securityService?.GetUserId().ToString();
                    if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                    unitOfWork.BeginTransaction();

                    var userNotification =
                        await repo.GetByUserIdAndNotificationIdAsync(userId, notificationMessageId, cancellationToken);
                    if (userNotification == null)
                    {
                        return Results.NotFound();
                    }

                    await service.ReadNotification(userNotification, cancellationToken);
                    unitOfWork.Commit();
                    return Results.Ok(NotificationMessageDto.Map(userNotification));
                })
                .WithSummary($"Read notification")
                .WithDescription($"# set notification as read")
                .WithName($"ReadNotificationMessage")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK, typeof(NotificationMessageDto))
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();

            endpointRouteBuilder.MapGet("notifications", async (
                    [FromServices] INotificationServices<string> service,
                    [FromServices] TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
                    [AsParameters] UserNotificationFilter filter, // need to fix binding
                    [AsParameters] TabTabGo.Core.ViewModels.PagingOptionRequest page, // need to fix binding
                    HttpRequest request
                ) =>
                {
                    var userId = securityService?.GetUserId().ToString();
                    if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                    var result = await service.GetUserNotifications(userId
                        //how to get Current user ??? do we need to use tabtabgo.ISecureityService or add new Service 
                        , filter, page);

                    return Results.Ok(new PageList<NotificationMessageDto>(
                        result.Items.Select(NotificationMessageDto.Map).ToArray(),
                        result.TotalItems, result.PageSize, result.PageNumber
                    ));
                }).WithSummary($"search notifications")
                .WithDescription($"# search about notification")
                .WithName($"SearchNotificationMessages")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK, typeof(PageList<NotificationMessageDto>))
                .WithOpenApi();
            ;

            endpointRouteBuilder.MapGet("notifications/{notificationMessageId}",
                    (
                        HttpRequest request,
                        [FromServices] INotificationUserRepository repo,
                        [FromServices] INotificationServices<string> service,
                        [FromServices] TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
                        Guid notificationMessageId) =>
                    {
                        var userId = securityService?.GetUserId().ToString();
                        if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                        var userNotification = repo.GetByUserIdAndNotificationId(userId, notificationMessageId);
                        if (userNotification == null)
                        {
                            return Results.NotFound();
                        }

                        return Results.Ok(NotificationMessageDto.Map(userNotification));
                    }
                ).WithSummary($"Get notification")
                .WithDescription($"# Get notification")
                .WithName($"GetNotificationMessage")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK, typeof(NotificationMessageDto))
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();
            ;
            return endpointRouteBuilder;
        }
    }
}