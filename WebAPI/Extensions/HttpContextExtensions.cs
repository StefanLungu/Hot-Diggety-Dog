using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Resources;

namespace WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task InsertPaginationParameterInResponse<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
        {
            double count = await queryable.CountAsync();
            double numberOfPages = Math.Ceiling(count / recordsPerPage);
            httpContext.Response.Headers.Add(Constants.NumberOfPagesHeader, numberOfPages.ToString());
        }
    }
}
