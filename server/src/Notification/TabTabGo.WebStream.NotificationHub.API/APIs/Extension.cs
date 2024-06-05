using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TabTabGo.Core.Data;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.NotificationHub.Entities;
using TabTabGo.WebStream.NotificationHub.Module;
using TabTabGo.WebStream.NotificationHub.Repository;
using TabTabGo.WebStream.NotificationHub.Services;

namespace TabTabGo.WebStream.NotificationHub.APIs
{
    public static class Extension
    {
        public static IEndpointRouteBuilder MapNotificationsEndPoints(
            this IEndpointRouteBuilder endpointRouteBuilder, string prefix)
        {
            const string tag = "TabTabGoNotification";
            endpointRouteBuilder.MapPost(prefix + "/notificaitons/{Id}/read",
                    (
                        [FromServices] INotificationUserRepository repo,
                        [FromServices] ISecurityService securityService,
                        [FromServices] IUnitOfWork unitOfWork,
                        [FromServices] INotificationServices service,
                        Guid Id) =>
                    {
                        unitOfWork.BeginTransaction();

                        var userNotification =
                            repo.GetByUserIdAndNotificationId(securityService.GetUser().UserId.ToString(), Id);
                        if (userNotification != null)
                        {
                            return Results.NotFound();
                        }

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

            endpointRouteBuilder.MapGet(prefix + "/notificaitons",
                    (
                        [FromServices] INotificationServices service,
                        [FromServices] INotificationUserRepository repo,
                        [FromServices] ISecurityService securityService,
                        [AsParameters] UserNotificationFilter filter, // need to fix binding
                        [AsParameters] TabTabGo.Core.ViewModels.PagingOptionRequest page // need to fix binding
                    ) =>
                    {
                        var result = service.GetUserNotifications(securityService.GetUser().UserId.ToString()
                            //how to get Current user ??? do we need to use tabtabgo.ISecureityService or add new Service 
                            , filter, page, repo);
                        return Results.Ok(result);
                    }).WithSummary($"search notifications")
                .WithDescription($"# search about notification")
                .WithName($"search_notifications")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK, typeof(PageList<NotificationUser>))
                .WithOpenApi();
            ;


            endpointRouteBuilder.MapGet(prefix + "/notificaitons/{Id}",
                    ([FromServices] INotificationUserRepository repo, [FromServices] ISecurityService securityService,
                        [FromServices] INotificationServices service, Guid Id) =>
                    {
                        var userNotification =
                            repo.GetByUserIdAndNotificationId(securityService.GetUser().UserId.ToString(), Id);
                        if (userNotification != null)
                        {
                            return Results.NotFound();
                        }

                        return Results.Ok(userNotification);
                    }
                ).WithSummary($"Get notification")
                .WithDescription($"# Get notification")
                .WithName($"get_notification")
                .WithTags(tag)
                .Produces(StatusCodes.Status200OK, typeof(NotificationUser))
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();
            ;
            return endpointRouteBuilder;
        }
    }
}