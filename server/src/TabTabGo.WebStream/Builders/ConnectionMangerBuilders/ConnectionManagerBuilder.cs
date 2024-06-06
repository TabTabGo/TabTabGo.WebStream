using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.Services.ConnectionManagerServices;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.PushEventsServices;

namespace TabTabGo.WebStream.Builders.ConnectionMangerBuilders
{
    public class ConnectionManagerBuilder
    {
        private readonly List<Func<IServiceProvider, IConnectionManager>> _connectionManagerBuilder = new List<Func<IServiceProvider, IConnectionManager>>();
        public ConnectionManagerBuilder AddConnectionManager(Func<IServiceProvider, IConnectionManager> func)
        {
            _connectionManagerBuilder.Add(func);
            return this;
        }
        public IConnectionManager Build(IServiceProvider serviceProvider)
        {
            return new ConnectionManager(_connectionManagerBuilder.Select(x => x(serviceProvider)));
        }
    }
}
