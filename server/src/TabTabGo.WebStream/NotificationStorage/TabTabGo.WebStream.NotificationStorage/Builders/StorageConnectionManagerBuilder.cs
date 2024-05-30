﻿using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders.ConnectionMangerBuilders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Builders
{
    public static class StorageConnectionManagerBuilder
    {
        /// <summary>
        /// by using this decoration, all  connections will be stored in dataBase. <br/>
        /// it is the implementation of IConnectionManager <br/>
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static ConnectionManagerBuilder AddConnectionToStorage(this ConnectionManagerBuilder webStreamBuilder)
        {
            webStreamBuilder.AddConnectionManager((serviceProvider) => new StorageConnectionManager(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>()));
            return webStreamBuilder;
        }
        
    }
}
