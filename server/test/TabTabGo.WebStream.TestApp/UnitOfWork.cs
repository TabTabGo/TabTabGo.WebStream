using Microsoft.EntityFrameworkCore;
using System.Data;
using TabTabGo.Core.Data;

namespace TabTabGo.WebStream.TestApp
{
    public class UnitOfWork : TabTabGo.Data.EF.UnitOfWork, IUnitOfWork
    {
        DbContext? _context;
        public UnitOfWork(DbContext? context, ILogger<TabTabGo.Data.EF.UnitOfWork> logger) : base(context, logger)
        {
            _context = context;
        }

        public override void Dispose()
        {
             
        }
    }
}
