using Microsoft.EntityFrameworkCore.Storage;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EFNotificationStorageTransaction(IDbContextTransaction dbContextTransaction) : INotificationTransaction
    {
        public void Commit()
        {
            dbContextTransaction.Commit();
        } 
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return dbContextTransaction.CommitAsync(cancellationToken);
        } 
        public void Dispose()
        {
            dbContextTransaction.Dispose();
        } 
        public void RollBack()
        {
            dbContextTransaction.Rollback();
        }

        public Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            return dbContextTransaction.RollbackAsync(cancellationToken);
        }
    }
}
