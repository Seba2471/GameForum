namespace GameForum.Application.Functions.Pagination
{
    public class PaginationResponse<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PaginationResponse(ICollection<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            if (totalCount < pageSize)
            {
                ItemsTo = totalCount;
            }
            else
            {
                ItemsTo = ItemsFrom + pageSize - 1;
            }
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
