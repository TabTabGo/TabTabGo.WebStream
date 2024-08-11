﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.ConnectionManagerServices
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly List<IConnectionManager> _connectionManagers = null;
        public ConnectionManager(IEnumerable<IConnectionManager> connectionManagers)
        {
            _connectionManagers = connectionManagers.ToList();
        }
        public void RegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            foreach (var cm in _connectionManagers)
            {
                cm.RegisterConnection(connectionId, userId, parameters);
            }
        }

        public async Task RegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            foreach (var cm in _connectionManagers)
            {
                await cm.RegisterConnectionAsync(connectionId, userId,parameters, cancellationToken);
            }
        }

        public void ReRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            foreach (var cm in _connectionManagers)
            {
                cm.ReRegisterConnection(connectionId, userId);
            }
        }

        public async Task ReRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            foreach (var cm in _connectionManagers)
            {
                await cm.ReRegisterConnectionAsync(connectionId, userId, cancellationToken: cancellationToken);
            }
        }

        public void UnRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            foreach (var cm in _connectionManagers)
            {
                cm.UnRegisterConnection(connectionId, userId);
            }
        }

        public async Task UnRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            foreach (var cm in _connectionManagers)
            {
                await cm.UnRegisterConnectionAsync(connectionId, userId,cancellationToken: cancellationToken);
            }
        }
    }
}
