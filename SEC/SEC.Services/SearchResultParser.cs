using SEC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEC.Services
{
    public class SearchResultParser: ISearchResultParser
    {

        public IList<int> ParseSearchResult(string searchResult)
        {
            var result = new List<int>();
            // TODO: there should be a better way to parse the search result, just use these 2 key words to parse for the test project
            var keywordStart = "<a href=\"/url?q=";
            var keywordEnd = "&amp;sa=U&amp;";
            if (string.IsNullOrEmpty(searchResult) || searchResult.IndexOf(keywordStart) < 0)
                return result;

            var keywordStartLen = keywordStart.Length;
            var sub = searchResult.Substring(searchResult.IndexOf(keywordStart) + keywordStartLen);
            var urls = new List<string>();
            var index = 0;
            
            while (!string.IsNullOrEmpty(sub) && sub.Length > keywordStartLen && index < 100)
            {
                if (sub.IndexOf(keywordEnd) <= 0)
                    break;

                var url = sub.Substring(0, sub.IndexOf(keywordEnd));
                urls.Add(url);
                index++;

                if (sub.IndexOf(keywordStart) <= 0)
                    break;
                sub = sub.Substring(sub.IndexOf(keywordStart) + keywordStartLen);
            }

            // check the smoke ball url places
            var smokeballUrl = ConfigurationManager.AppSettings["SmokeBallUrl"];
            index = 1;
            foreach(var url in urls)
            {
                if(!string.IsNullOrEmpty(url) && url.ToLower().Contains(smokeballUrl))
                {
                    result.Add(index);
                }
                index++;
            }

            return result;
        }

        
    }
}
