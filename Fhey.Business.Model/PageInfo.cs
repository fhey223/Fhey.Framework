namespace Fhey.Business.Model
{
    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }

        public PageInfo(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PageInfo(int pageIndex, int pageSize, int totalCount) : this(pageIndex, pageSize)
        {
            PageCount = 0;
            TotalCount = totalCount;
            if (pageSize > 0)
            {
                PageCount = (int)(totalCount / pageSize) + ((totalCount % pageSize) > 0 ? 1 : 0);
            }
        }
    }
}
