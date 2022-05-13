using Castle.Windsor;
using SEC.Services.Interfaces;
using SEC.UI.Dependency;
using SEC.Utilities.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace SEC.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISearchResultParser _searchResultParser;
        private readonly IHttpWebClient _httpWebClient;
        public MainWindow()
        {
            InitializeComponent();

            //IoC install
            var container = new WindsorContainer();
            (new DependencyResolver()).Install(container);

            _searchResultParser = container.Resolve<ISearchResultParser>();
            _httpWebClient = container.Resolve<IHttpWebClient>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var keyWord = TBkeyWord.Text;

            if(string.IsNullOrEmpty(keyWord))
            {
                var errorMsg = "Please input search key word.";
                LBResult.Content = errorMsg;
                return;
            }

            var searchEnginUrl = ConfigurationManager.AppSettings["SearchEnginUrl"];
            var searStr = searchEnginUrl + "&q=" + HttpUtility.UrlEncode(keyWord);        
            
            var searchResult = Task.Run(async () => await _httpWebClient.SendGetAsync(searStr)).Result;         
            var parserResult = _searchResultParser.ParseSearchResult(searchResult);

            LBResult.Content = PrepareUIMsg(parserResult);        
        }

        private string PrepareUIMsg(IList<int> parserResult)
        {
            var resultMsg = "";
            if (parserResult.Count == 0)
                resultMsg = "There are no SmokeBall url in the search result.";
            else
            {
                var strBuild = new StringBuilder("The SmokeBall url is in the search result: \n");
                strBuild.Append("Line ");
                strBuild.Append(string.Join(", ", parserResult.ToArray()));
                resultMsg = strBuild.ToString();
            }

            return resultMsg;
        }
    }
}
