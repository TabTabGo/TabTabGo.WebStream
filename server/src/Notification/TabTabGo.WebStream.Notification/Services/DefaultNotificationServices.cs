using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.NotificationHub.Entities;
using TabTabGo.WebStream.NotificationHub.Entities.Enums;
using TabTabGo.WebStream.NotificationHub.Module;
using TabTabGo.WebStream.NotificationHub.Repository;

namespace TabTabGo.WebStream.NotificationHub.Services
{
    public class DefaultNotificationServices : INotificationServices
    {
        List<Expression<Func<NotificationUser, bool>>> GetNotificationUserCriteria(UserNotificationFilter filters)
        {
            var criteria = new List<Expression<Func<NotificationUser, bool>>>();
            if (filters != null)
            {
                if (string.IsNullOrEmpty(filters.Q))
                {
                    filters.Q = filters.Q.ToLower();
                    criteria.Add(s => s.Notification.EventName.ToLower().Contains(filters.Q));
                }

                if (string.IsNullOrEmpty(filters.EventsNames))
                {
                    var list = filters.EventsNames.ToLower().Split('|').ToList();
                    criteria.Add(s => list.Contains(s.Notification.EventName));
                }
                if (string.IsNullOrEmpty(filters.Status))
                {
                    var list = filters.Status.ToLower().Split('|').Where(s => Enum.TryParse<NotificationUserStatus>(s, true, out var _)).Select(s =>(NotificationUserStatus) Enum.Parse(typeof(NotificationUserStatus),s, true)).ToList();
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
        public PageList<NotificationUser> GetUserNotifications(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository)
        {
            var criteria = GetNotificationUserCriteria(filters);
            criteria.Add(s => s.UserId.Equals(userId));
            return notificationUserRepository.GetPageList(criteria, pagingParameters.OrderBy, pagingParameters.OrderDirection.ToLower().Equals("desc"), pagingParameters.PageSize, pagingParameters.Page);
        } 
        public Task<PageList<NotificationUser>> GetUserNotificationsAsync(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default)
        {
            var criteria = GetNotificationUserCriteria(filters);
            criteria.Add(s => s.UserId.Equals(userId));
            return notificationUserRepository.GetPageListAsync(criteria, pagingParameters.OrderBy, pagingParameters.OrderDirection.ToLower().Equals("desc"), pagingParameters.PageSize, pagingParameters.Page);
        }
        public void ReadNotification(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository)
        {
            notificationUser.ReadDateTime = DateTime.UtcNow;
            notificationUser.Status = NotificationUserStatus.Read;
            notificationUserRepository.Update(notificationUser);
        } 
        public Task ReadNotificationAsync(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default)
        {
            notificationUser.ReadDateTime = DateTime.UtcNow;
            notificationUser.Status = NotificationUserStatus.Read;
            return notificationUserRepository.UpdateAsync(notificationUser, cancellationToken);
        }
    }
}
