using Microsoft.EntityFrameworkCore;
using TabTabGo.Core.Data;

namespace TabTabGo.WebStream.TestApp
{
    public class UnitOfWork : TabTabGo.Data.EF.UnitOfWork, IUnitOfWork
    {
        public UnitOfWork(DbContext? context, ILogger<TabTabGo.Data.EF.UnitOfWork> logger) : base(context, logger)
        {
        }

        public override void Dispose()
        {
            
        }
    }
}
