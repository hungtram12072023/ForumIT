using Microsoft.EntityFrameworkCore;

namespace ForumIT.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPrePage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPage;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int PageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, PageIndex, pageSize);
        }


    }
}
