using System.Linq.Expressions;

namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class LinqExtension
    {
        public static IOrderedQueryable<TSource> Order<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> expression, bool descending)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (!descending)
                return source.OrderBy(expression);
            else
                return source.OrderByDescending(expression);
        }
    }
}
