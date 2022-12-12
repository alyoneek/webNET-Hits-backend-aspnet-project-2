using Microsoft.EntityFrameworkCore;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PageInfoModel Pagination { get; set; }

        public PaginatedList(List<T> items, PageInfoModel pageInfo)
        {
            Pagination = pageInfo;

            this.AddRange(items);
        }

        public bool HasPreviousPage => Pagination.Current > 1;
        public bool HasNextPage => Pagination.Current < Pagination.Count;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PageInfoModel pageInfo)
        {
            var items = await source.Skip((pageInfo.Current - 1) * pageInfo.Size).Take(pageInfo.Size).ToListAsync();
            return new PaginatedList<T>(items, pageInfo);
        }
    }
}
