using System;

namespace MvcPL.Models.Pagination
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
        public int Skip => (PageNumber - 1) * PageSize;
        public string UrlPart { get; set; }
    }
}