using System;
using System.Linq;
using System.Linq.Expressions;

namespace TabTabGo.WebStream.NotificationStorage
{
    public static class QueryExt
    {
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool desc = true)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return query;
            }

            var lambda = (dynamic)CreateExpression(typeof(TSource), key); 
            return desc ?  Queryable.OrderByDescending(query, lambda) :  Queryable.OrderBy(query, lambda); 
        }
        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            return Expression.Lambda(body, param);
        }
    }
}
