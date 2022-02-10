using AutoMapper;

namespace [$project_name].utils.helpers
{
    public class PaginationSet<T>
    {
        public int Page { set; get; }

        [IgnoreMap]
        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }

        [IgnoreMap]
        public int TotalPages
        { get { return PageSize != 0 ? (int)Math.Ceiling((decimal)TotalCount / PageSize) : 0; } }

        public long TotalCount { set; get; }
        public int PageSize { set; get; }
        public IEnumerable<T> Items { set; get; }
        public string Keyword { get; set; }
    }
}