using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TabTabGo.WebStream.NotificationHub.Repository
{
    public static class QueryHelper
    {
        public static IQueryable<T> AppleyCriteria<T>(this IQueryable<T> query, List<Expression<Func<T, bool>>> criteria)
        {
            foreach (var item in criteria ?? new List<Expression<Func<T, bool>>>())
            {
                query = query.Where(item);
            }
            return query;
        } 
    }
}
