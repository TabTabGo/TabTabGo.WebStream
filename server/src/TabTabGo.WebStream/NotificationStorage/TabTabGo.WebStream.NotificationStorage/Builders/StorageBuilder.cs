using System;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage
{
    public class StorageBuilder
    {

        Func<INotificationUnitOfWork> _UnitOfWorkFunction;
        public StorageBuilder SetUnitOfWork(Func<INotificationUnitOfWork> UnitOfWorkFunction)
        {
            _UnitOfWorkFunction = UnitOfWorkFunction;
            return this;
        }
        public INotificationUnitOfWork GetUnitOfWork()
        {
            return _UnitOfWorkFunction();
        }
    }
}
