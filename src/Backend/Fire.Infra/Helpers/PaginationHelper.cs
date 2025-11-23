using Fire.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Fire.Infra.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PagedList<T>> CreateAsync<T>
            (IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var itens = await source.Skip((pageNumber - 1) * pageSize).Take((pageSize)).ToListAsync();
            return new PagedList<T>(itens, pageNumber, pageSize, count);
        }
    }
}
