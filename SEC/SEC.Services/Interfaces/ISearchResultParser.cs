using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEC.Services.Interfaces
{
    public interface ISearchResultParser
    {
        IList<int> ParseSearchResult(string searchResult);
    }
}
