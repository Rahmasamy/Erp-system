using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire.Utilities;

public class PaginationModel<T> where T : class
{
    //public List<T> Data { get; set; }
    public IEnumerable<T> Items { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));
    public Dictionary<string, decimal>? Aggregates { get; set; }

    public PaginationModel(IEnumerable<T> Items, int pageNumber, int pageSize, int totalItems, Dictionary<string, decimal> aggs)
    {
        this.Items = Items;
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.TotalItems = totalItems;
        this.Aggregates = aggs;
    }
}
