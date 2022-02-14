using RefactoringChallenge.Business.Paging;
using System.Linq;

namespace RefactoringChallenge.Business.Extensions
{
    public static class QueryableExtensions
    {
        public static PagingResult<T> ToPagingResult<T>(this IQueryable<T> query, int? skip, int? take)
        {
            var total = query.Count();
            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }
            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return new PagingResult<T>
            {
                PageData = query.ToList(),
                Total = total,
                PageSize = take ?? 0
            };
        }
    }
}