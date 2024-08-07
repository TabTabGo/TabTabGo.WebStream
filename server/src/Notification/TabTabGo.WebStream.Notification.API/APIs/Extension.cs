using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using TabTabGo.Core.Data;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
namespace TabTabGo.WebStream.Notification.API.APIs
{
    public static class Extension
    {
        public static IEndpointRouteBuilder MapNotificationsEndPoints<TUserKey, TTenantKey>(this IEndpointRouteBuilder endpointRouteBuilder, string prefix) where TUserKey : struct where TTenantKey : struct
        {
            const string tag = "Notification";
            endpointRouteBuilder.MapPost(prefix + "/notifications/{notificationMessageId}/read",
                (
                [FromServices] INotificationUserRepository repo,
                [FromServices] IUnitOfWork unitOfWork,
                [FromServices] INotificationServices service,
                [FromServices] TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
                HttpRequest request,
                Guid notificationMessageId) =>
            {
                var userId = securityService?.GetUserId().ToString();

                var tenantId = securityService?.GetTenantId().ToString();
                if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                unitOfWork.BeginTransaction();

                var userNotification = repo.GetByUserIdAndNotificationId(UserIdData.From(userId,tenantId), notificationMessageId);
                if (userNotification == null) { return Results.NotFound(); }
                service.ReadNotification(userNotification, repo);
                unitOfWork.Commit();
                return Results.Ok(userNotification);
            })
            .WithSummary($"Read notification")
            .WithDescription($"# set notification as read")
            .WithName($"Read_notification")
            .WithTags(tag)
            .Produces(StatusCodes.Status200OK, typeof(NotificationUser))
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            endpointRouteBuilder.MapGet(prefix + "/notifications",
                 (
                 [FromServices] INotificationServices service,
                 [FromServices] INotificationUserRepository repo, 
                 [FromServices] TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
                 [AsParameters] UserNotificationFilter filter,  // need to fix binding
                 [AsParameters] TabTabGo.Core.ViewModels.PagingOptionRequest page,// need to fix binding
                 HttpRequest request
            ) =>
             {

                 var userId = securityService?.GetUserId().ToString();
                 var telenetId = securityService?.GetTenantId().ToString();
                 if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                 var result = service.GetUserNotifications(UserIdData.From(userId,telenetId)
                //how to get Current user ??? do we need to use tabtabgo.ISecureityService or add new Service 
                , filter, page, repo);
                 return Results.Ok(result);

             }).WithSummary($"search notifications")
            .WithDescription($"# search about notification")
            .WithName($"search_notifications")
            .WithTags(tag)
            .Produces(StatusCodes.Status200OK, typeof(PageList<NotificationUser>))
            .WithOpenApi(); ;


            endpointRouteBuilder.MapGet(prefix + "/notifications/{notificationMessageId}", 
                (
                    HttpRequest request,  
                    [FromServices] INotificationUserRepository repo,
                    [FromServices] INotificationServices service,
                    [FromServices] TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
                    Guid notificationMessageId) =>
            {

                var userId = securityService?.GetUserId().ToString();
                var tenant = securityService?.GetTenant().ToString();
                if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                var userNotification = repo.GetByUserIdAndNotificationId(UserIdData.From(userId,tenant), notificationMessageId);
                if (userNotification == null) { return Results.NotFound(); }
                return Results.Ok(userNotification);
            }
          ).WithSummary($"Get notification")
            .WithDescription($"# Get notification")
            .WithName($"get_notification")
            .WithTags(tag)
            .Produces(StatusCodes.Status200OK, typeof(NotificationUser))
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(); ;
            return endpointRouteBuilder;
        }
    }
}
