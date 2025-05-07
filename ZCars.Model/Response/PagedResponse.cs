namespace ZhooCars.Model.Response
{
    public class PagedResponse<T>
    {
        #region Properties

        public IEnumerable<T> Data { get; set; } = new List<T>();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        #endregion
    }

}
