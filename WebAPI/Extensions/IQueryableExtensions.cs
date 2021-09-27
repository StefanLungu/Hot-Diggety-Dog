using Domain.Dtos;
using System.Linq;

namespace WebApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            return queryable.Skip((pagination.Page - 1) * pagination.EntitiesPerPage).Take(pagination.EntitiesPerPage);
        }
    }
}
