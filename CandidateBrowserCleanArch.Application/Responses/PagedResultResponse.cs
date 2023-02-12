using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Responses;

public class PagedResultResponse<T> : BaseResponse
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public double TotalPages => TotalCount == 0 ? 1 : Math.Ceiling((double)(TotalCount / PageSize));
    public int PageSize { get; set; }
    public IEnumerable<T> Items { get; set; }
}
