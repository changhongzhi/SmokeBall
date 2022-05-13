using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEC.Utilities.Interfaces
{
    public interface IHttpWebClient
    {
        Task<string> SendGetAsync(string url);
    }
}
