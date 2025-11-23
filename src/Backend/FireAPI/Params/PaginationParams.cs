using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fire.API.Params
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int pageNumber { get; set; }
        [Range(1, 50, ErrorMessage = "O limite máximo de itens por página é 50.")]
        [DefaultValue(10)]
        public int pageSize { get; set; }
    }
}
