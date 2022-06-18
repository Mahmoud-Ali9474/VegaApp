namespace Vega.API.Core.Models
{
    public class Query
    {
        public string? SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        private int _page;
        private int _pageSize;

        public int Page { get { return _page; } set { _page = Math.Max(1, value); } }
        public int PageSize { get { return _pageSize; } set { _pageSize = Math.Max(value, 3); } }

    }
}
