using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire.Utilities;

public class ListsRequest
{
    public int First { get; set; }
    public int PageSize { get; set; }
    public string? SearchQuery { get; set; }
    public string? Entity { get; set; }    
    public string? WhrCond { get; set; } 
    public string? SelectedId { get; set; }

}

public class OptionsRequest
{
    public string? Field { get; set; }
    public int First { get; set; }
    public int PageSize { get; set; }
    public string? WhrCond { get; set; }
    public string? EntityName { get; set; }
    public string? SearchQuery { get; set; }
}

public class ListsResponse
{
    public List<UnifiedListDTO> Options { get; set; } = [];
}

public class OptionsResponse
{
    public List<UnifiedDTO> Options { get; set; } = [];
}

