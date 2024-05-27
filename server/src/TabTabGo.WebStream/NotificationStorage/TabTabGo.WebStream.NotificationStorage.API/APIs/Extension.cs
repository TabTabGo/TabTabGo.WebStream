using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TabTabGo.Core.Data;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;
namespace TabTabGo.WebStream.NotificationStorage.API.APIs
{
    public static class Extension
    {
        public static IEndpointRouteBuilder MapTabtabGoNotificationsEndPoints(this IEndpointRouteBuilder endpointRouteBuilder, string prefix)
        {
            endpointRouteBuilder.MapPost(prefix + "/notificaitons/{Id}/read",
                ([FromServices] INotificationUserRepository repo, [FromServices] ISecurityService securityService, [FromServices] IUnitOfWork unitOfWork, [FromServices] INotificationServices service, Guid Id) =>
            {
                unitOfWork.BeginTransaction();

                var userNotification = repo.FindByUserIdAndNotificationId(securityService.GetUser().UserId.ToString(), Id);
                if (userNotification != null) { return Results.NotFound(); }
                service.ReadNotification(userNotification, repo);
                unitOfWork.Commit();
                return Results.Ok(userNotification);
            });

            /* endpointRouteBuilder.MapGet(prefix+"/notificaitons",
                 ([FromServices] INotificationServices service,
                 [FromServices] INotificationUnitOfWorkFactory unitOfWorkFactory,
                 [FromQuery("")] UserNotificationFilter filter,  // need to fix binding
                 [FromQuery("page")]PagingParameters page// need to fix binding
            
            ) =>
             {
                 using (var unitofWork = unitOfWorkFactory.Get())
                 {

                     var result = service.GetUserNotifications("currentUser"
                                //how to get Current user ??? do we need to use tabtabgo.ISecureityService or add new Service 
                    , filter, page, unitofWork.UserRepository);
                     return Results.Ok(result);
                 }
             });
            */


            endpointRouteBuilder.MapGet(prefix + "/notificaitons/{Id}", ([FromServices] INotificationUserRepository repo, [FromServices] ISecurityService securityService, [FromServices] INotificationServices service, Guid Id) =>
            {

                var userNotification = repo.FindByUserIdAndNotificationId(securityService.GetUser().UserId.ToString(), Id);
                if (userNotification != null) { return Results.NotFound(); }
                return Results.Ok(userNotification);
            }
          );
            return endpointRouteBuilder;
        }
    }
}
