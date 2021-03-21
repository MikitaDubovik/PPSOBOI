using System.Collections.Generic;

namespace MvcPL.Models.Pagination
{
    public class PaginationViewModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}