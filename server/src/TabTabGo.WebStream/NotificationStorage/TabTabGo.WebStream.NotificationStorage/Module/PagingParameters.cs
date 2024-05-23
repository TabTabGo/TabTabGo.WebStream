using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.NotificationStorage.Module
{
    public class PagingParameters
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Order { get; set; }
        public bool IsDesc { get; set; }
    }
}
