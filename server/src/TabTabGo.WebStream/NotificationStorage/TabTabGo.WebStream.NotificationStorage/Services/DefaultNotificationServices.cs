﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Entites.Enums;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    internal class DefaultNotificationServices() : INotificationServices
    {
        List<Expression<Func<NotificationUser, bool>>> getNotificationUserCriteria(UserNotificationFilter filters)
        {
            var criteria = new List<Expression<Func<NotificationUser, bool>>>();
            if (filters != null)
            {
                if (string.IsNullOrEmpty(filters.q))
                {
                    filters.q = filters.q.ToLower();
                    criteria.Add(s => s.Notification.EventName.ToLower().Contains(filters.q));
                }

                if (string.IsNullOrEmpty(filters.EventsNames))
                {
                    var list = filters.EventsNames.ToLower().Split('|').ToList();
                    criteria.Add(s => list.Contains(s.Notification.EventName));
                }
                if (string.IsNullOrEmpty(filters.Status))
                {
                    var list = filters.Status.ToLower().Split('|').Where(s => Enum.TryParse<NotificationUserStatus>(s, true, out var _)).Select(s => Enum.Parse<NotificationUserStatus>(s, true)).ToList();
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

        public PageingResult<NotificationUser> GetUserNotifications(string userId, UserNotificationFilter filters, PagingParameters pagingParameters, INotificationUserRepository notificationUserRepository)
        {
            var criteria = getNotificationUserCriteria(filters);
            criteria.Add(s => s.UserId.Equals(userId));
            return notificationUserRepository.FindByCriteria(criteria, pagingParameters.Order, pagingParameters.IsDesc, pagingParameters.PageSize, pagingParameters.PageNumber);
        }

        public Task<PageingResult<NotificationUser>> GetUserNotificationsAsync(string userId, UserNotificationFilter filters, PagingParameters pagingParameters, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default)
        {
            var criteria = getNotificationUserCriteria(filters);
            criteria.Add(s => s.UserId.Equals(userId));
            return notificationUserRepository.FindByCriteriaAsync(criteria, pagingParameters.Order, pagingParameters.IsDesc, pagingParameters.PageSize, pagingParameters.PageNumber, cancellationToken);

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