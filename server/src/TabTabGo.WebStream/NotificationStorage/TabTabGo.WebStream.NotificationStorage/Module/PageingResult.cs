using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.NotificationStorage.Module
{
    public class PagingException : ApplicationException
    {
        public PagingException()
        {

        }
        public PagingException(string? message) : base(message)
        {

        }
        public PagingException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
    public class PageingResultBuilder<T>
    {
        protected readonly bool NeedOrder;
        protected readonly IQueryable<T> Query;
        protected readonly int PageNumber;
        protected readonly int PageSize;
        protected readonly string Order;
        protected readonly bool IsDesc;
        public PageingResultBuilder(IQueryable<T> query, int pageNumber, int pageSize, string order, bool isDesc) : this(query, pageNumber, pageSize)
        {
            this.NeedOrder = true;
            this.Order = order;
            this.IsDesc = isDesc;

        }
        public PageingResultBuilder(IQueryable<T> query, int pageNumber, int pageSize)
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


        public virtual PageingResult<T> Build()
        {
            var countTo = (PageNumber + 5) * PageSize;
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
            return new PageingResult<T> { Items = result, PartialTotal = partialCount, pageNumber = this.PageNumber, pageSize = this.PageSize }; 

        }
        public virtual Task<PageingResult<T>> BuildAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run<PageingResult<T>>(() => this.Build(), cancellationToken);
        }
    }

    public class PageingResult<T>
    {
        public int PartialTotal { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<T> Items { get; set; }
    }

}
