namespace Fire.API.Params
{
    public class PaginationHeader
    {
        public int currentPage { get; set; }
        public int itensPerPage { get; set; }
        public int totalItens { get; set; } // total de itens da consulta toda
        public int totalPages { get; set; }

        public PaginationHeader(int currentPage, int itensPerPage, int totalItens, int totalPages)
        {
            this.currentPage = currentPage;
            this.itensPerPage = itensPerPage;
            this.totalItens = totalItens;
            this.totalPages = totalPages;
        }
    }
}
