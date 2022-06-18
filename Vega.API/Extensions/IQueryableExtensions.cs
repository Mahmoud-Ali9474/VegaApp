using System.Linq.Expressions;
using Vega.API.Core.Models;

namespace Vega.API.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, Query queryObj, Dictionary<string, Expression<Func<T, object>>> sortableColumnsExpressions)
        {

            if (string.IsNullOrEmpty(queryObj.SortBy) || !sortableColumnsExpressions.ContainsKey(queryObj.SortBy)) return query;

            query = queryObj.IsSortAscending ? query.OrderBy(sortableColumnsExpressions.GetValueOrDefault(queryObj.SortBy))
            : query.OrderByDescending(sortableColumnsExpressions.GetValueOrDefault(queryObj.SortBy));

            return query;
        }
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Query queryObj)
        {
            query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
            return query;
        }
    }
}