using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.MessageStorage
{
    public class PagingException : ApplicationException
    {
        public PagingException()
        {

        }
        public PagingException(string message) : base(message)
        {

        }
        public PagingException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
    public class PageingListBuilder<T> where T : class
    {
        protected readonly bool NeedOrder;
        protected readonly IQueryable<T> Query;
        protected readonly int PageNumber;
        protected readonly int PageSize;
        protected readonly string Order;
        protected readonly bool IsDesc;
        public PageingListBuilder(IQueryable<T> query, int pageNumber, int pageSize, string order, bool isDesc) : this(query, pageNumber, pageSize)
        {
            this.NeedOrder = true;
            this.Order = order;
            this.IsDesc = isDesc;

        }
        public PageingListBuilder(IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new PagingException("pageNumber should be > 0");
            }
            if (pageSize <= 0)
            {
                throw new PagingException("pageSize should be > 0");
            }

            this.Query = query;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }


        public virtual TabTabGo.Core.Models.PageList<T> BuildWithPartialCount(int pagesToCountAfterThisPage)
        {
            var countTo = (PageNumber + pagesToCountAfterThisPage) * PageSize;
            var partialCount = Query.Take(countTo).Count(); 
            var queryToGetReuslt = Query;
            try
            {
                if (NeedOrder)
                {
                    queryToGetReuslt = queryToGetReuslt.OrderBy(Order, IsDesc);
                }
            }
            catch { }
            var result = queryToGetReuslt.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            return new TabTabGo.Core.Models.PageList<T>(result, partialCount,this.PageSize,this.PageNumber);
        }
        public virtual TabTabGo.Core.Models.PageList<T> BuildWithFullCount()
        { 
            var count = Query.Count();
            var queryToGetReuslt = Query;
            try
            {
                if (NeedOrder)
                {
                    queryToGetReuslt = queryToGetReuslt.OrderBy(Order, IsDesc);
                }
            }
            catch { }
            var result = queryToGetReuslt.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            return new TabTabGo.Core.Models.PageList<T>(result, count, this.PageSize, this.PageNumber);

        }


        public virtual Task<TabTabGo.Core.Models.PageList<T>> BuildWithPartialCountAsync(int pagesToCountAfterThisPage,CancellationToken cancellationToken = default)
        {
            return Task.Run<TabTabGo.Core.Models.PageList<T>>(() => this.BuildWithPartialCount(pagesToCountAfterThisPage), cancellationToken);
        }
        public virtual Task<TabTabGo.Core.Models.PageList<T>> BuildWithFullCountAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run<TabTabGo.Core.Models.PageList<T>>(() => this.BuildWithFullCount(), cancellationToken);
        }

    }
 

}
