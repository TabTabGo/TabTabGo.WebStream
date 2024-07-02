using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Entities.Enums;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;

namespace TabTabGo.WebStream.Notification.Services
{
    public class DefaultNotificationServices(INotificationUserRepository notificationUserRepository)
        : INotificationServices<string>
    {
        List<Expression<Func<NotificationUser, bool>>> GetNotificationUserCriteria(UserNotificationFilter filters)
        {
            var criteria = new List<Expression<Func<NotificationUser, bool>>>();
            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.Q))
                {
                    filters.Q = filters.Q.ToLower();
                    criteria.Add(s => s.NotificationMessage.EventName.ToLower().Contains(filters.Q));
                }

                if (!string.IsNullOrEmpty(filters.EventsNames))
                {
                    var list = filters.EventsNames.ToLower().Split('|').ToList();
                    criteria.Add(s => list.Contains(s.NotificationMessage.EventName));
                }

                if (!string.IsNullOrEmpty(filters.Status))
                {
                    var list = filters.Status.ToLower().Split('|')
                        .Where(s => Enum.TryParse<NotificationUserStatus>(s, true, out var _)).Select(s =>
                            (NotificationUserStatus)Enum.Parse(typeof(NotificationUserStatus), s, true)).ToList();
                    criteria.Add(s => list.Contains(s.Status));
                }

                if (filters.NotifiedDateTimeRangeStart.HasValue)
                {
                    criteria.Add(s => s.NotifiedDateTime >= filters.NotifiedDateTimeRangeStart.Value);
                }

                if (filters.NotifiedDateTimeRangeEnd.HasValue)
                {
                    criteria.Add(s => s.NotifiedDateTime <= filters.NotifiedDateTimeRangeEnd.Value);
                }

                if (filters.ReadDateRangeStart.HasValue)
                {
                    criteria.Add(s => s.ReadDateTime >= filters.ReadDateRangeStart.Value);
                }

                if (filters.ReadDateRangeEnd.HasValue)
                {
                    criteria.Add(s => s.ReadDateTime <= filters.ReadDateRangeEnd.Value);
                }
            }

            return criteria;
        }
     
        public  Task<PageList<NotificationUser>> GetUserNotifications(string userId,
            UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters,
            CancellationToken cancellationToken = default)
        {
            var criteria = GetNotificationUserCriteria(filters);
            criteria.Add(s => s.UserId.Equals(userId));

            return notificationUserRepository.GetPageListAsync(criteria,
                pagingParameters.OrderBy,
                pagingParameters.OrderDirection != null && pagingParameters.OrderDirection.ToLower().Equals("desc"),
                pagingParameters.PageSize, pagingParameters.Page, cancellationToken);
        }
     
        public Task ReadAllNotifications(string userId,
            CancellationToken cancellationToken = default)
        {
           return notificationUserRepository.UpdateAllUnreadNotificationsAsync(userId, NotificationUserStatus.Read,
                DateTime.UtcNow);
        }
        
        public Task ReadNotification(NotificationUser notificationUser,
            CancellationToken cancellationToken = default)
        {
            notificationUser.ReadDateTime = DateTime.UtcNow;
            notificationUser.Status = NotificationUserStatus.Read;
            return notificationUserRepository.UpdateAsync(notificationUser, cancellationToken);
        }
    }
}