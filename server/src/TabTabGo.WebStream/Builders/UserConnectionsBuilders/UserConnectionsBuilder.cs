using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.ConnectionMangerBuilders;
using TabTabGo.WebStream.Services.ConnectionManagerServices;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.UserConnectionsServices;

namespace TabTabGo.WebStream.Builders.UserConnectionsBuilders
{
    public class UserConnectionsBuilder
    {
        private readonly List<Func<IServiceProvider, IUserConnections>> _userConnectionsBuilder = new List<Func<IServiceProvider, IUserConnections>>();
        public UserConnectionsBuilder AddUserConnections(Func<IServiceProvider, IUserConnections> func)
        {
            _userConnectionsBuilder.Add(func);
            return this;
        }
        public IUserConnections Build(IServiceProvider serviceProvider)
        {
            return new UserConnections(_userConnectionsBuilder.Select(x => x(serviceProvider)));
        }
    }
}
