using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class QueryParameters
{
    private int _pageSize = 5;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set { _pageSize = value; }
    }
}
