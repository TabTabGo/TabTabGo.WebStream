using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.Core.Data;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public class UnitOfWork : TabTabGo.Data.EF.UnitOfWork, IUnitOfWork
    {
        public UnitOfWork(DbContext? context, ILogger<TabTabGo.Data.EF.UnitOfWork> logger) : base(context, logger)
        {
        }
    }
}
