namespace Fire.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int pageNumber { get; set; }//Numero da pagina que estou atualmente
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        //public int skip {  get; set; }
        //public int take { get; set; }

        public PagedList(IEnumerable<T> itens ,int pageNumber, int pageSize, int totalCount)
        {
            this.totalPages = (int) Math.Ceiling(totalCount/ (double) pageSize);
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.totalCount = totalCount;

            AddRange(itens);
        }

        public PagedList(IEnumerable<T> itens, int pageNumber, int pageSize, int totalPages, int totalCount)
        {
            this.totalPages = totalPages;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.totalCount = totalCount;

            AddRange(itens);
        }
    }
}
