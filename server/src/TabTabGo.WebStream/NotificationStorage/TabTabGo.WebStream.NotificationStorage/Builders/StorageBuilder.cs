using System;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage
{
    public class StorageBuilder
    {

        Func<INotificationUnitOfWorkFactory> _UnitOfWorkFunction;
        public StorageBuilder SetUnitOfWork(Func<INotificationUnitOfWorkFactory> UnitOfWorkFunction)
        {
            _UnitOfWorkFunction = UnitOfWorkFunction;
            return this;
        }
        public INotificationUnitOfWorkFactory GetUnitOfWork()
        {
            return _UnitOfWorkFunction();
        }
    }
}
