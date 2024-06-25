using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using TabTabGo.Core.Data;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
namespace TabTabGo.WebStream.Notification.API.APIs
{
    public static class Extension
    {
        public static IEndpointRouteBuilder MapNotificationsEndPoints(this IEndpointRouteBuilder endpointRouteBuilder, string prefix)
        {
            const string tag = "Notification";
            endpointRouteBuilder.MapPost(prefix + "/notifications/{Id}/read",
                (
                [FromServices] INotificationUserRepository repo,
                [FromServices] IUnitOfWork unitOfWork,
                [FromServices] INotificationServices service,
                HttpRequest request,
                Guid Id) =>
            {
                var userId = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                unitOfWork.BeginTransaction();

                var userNotification = repo.GetByUserIdAndNotificationId(userId, Id);
                if (userNotification != null) { return Results.NotFound(); }
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
                 [AsParameters] UserNotificationFilter filter,  // need to fix binding
                 [AsParameters] TabTabGo.Core.ViewModels.PagingOptionRequest page,// need to fix binding
                 HttpRequest request
            ) =>
             {

                 var userId = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                 if (string.IsNullOrEmpty(userId)) return Results.Forbid();
                 var result = service.GetUserNotifications(userId
                //how to get Current user ??? do we need to use tabtabgo.ISecureityService or add new Service 
                , filter, page, repo);
                 return Results.Ok(result);

             }).WithSummary($"search notifications")
            .WithDescription($"# search about notification")
            .WithName($"search_notifications")
            .WithTags(tag)
            .Produces(StatusCodes.Status200OK, typeof(PageList<NotificationUser>))
            .WithOpenApi(); ;


            endpointRouteBuilder.MapGet(prefix + "/notifications/{Id}", (HttpRequest request,  [FromServices] INotificationUserRepository repo,[FromServices] INotificationServices service, Guid Id) =>
            {

                var userId = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(string.IsNullOrEmpty(userId)) return Results.Forbid();
                var userNotification = repo.GetByUserIdAndNotificationId(userId, Id);
                if (userNotification != null) { return Results.NotFound(); }
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
