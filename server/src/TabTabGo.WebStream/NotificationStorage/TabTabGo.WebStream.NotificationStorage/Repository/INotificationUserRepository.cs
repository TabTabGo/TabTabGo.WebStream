﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationUserRepository
    {
        Guid Create(NotificationUser notificationUser);
        void Update(NotificationUser notificationUser);
        NotificationUser Find(Guid notificationUserId);
        Task<Guid> CreateAsync(NotificationUser notificationUser, CancellationToken cancellationToken = default);
        Task UpdateAsync(NotificationUser notificationUser, CancellationToken cancellationToken = default);
        Task<NotificationUser> FindAsync(Guid notificationUserId, CancellationToken cancellationToken = default);
        Task<List<NotificationUser>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        List<NotificationUser> FindByUserId(string userId);
        Task<NotificationUser> FindByUserIdAndNotificationIdAsync(string userId, Guid notificationId, CancellationToken cancellationToken = default);
        NotificationUser FindByUserIdAndNotificationId(string userId, Guid notificationId); 



        Task<List<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy,bool isDesc, CancellationToken cancellationToken = default);
        List<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc); 
        Task<PageingResult<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize,int pageNumber, CancellationToken cancellationToken = default);
        PageingResult<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber);


    }
}