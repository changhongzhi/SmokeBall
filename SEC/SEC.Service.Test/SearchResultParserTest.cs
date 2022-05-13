using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SEC.Services;
using SEC.Services.Interfaces;
using Assert = NUnit.Framework.Assert;

namespace SEC.Service.Test
{

    [TestFixture]
    public class SearchResultParserTest
    {
        private ISearchResultParser _searchResultParser;
        private string _searchResult;
        [SetUp]
        public void Start()
        {
            // TODO: should be generated from IoC Container
            _searchResultParser = new SearchResultParser();

            // the file content is the google search result in 13/05/2022
            string path = @".\SearchResult1.html";           
            _searchResult = File.ReadAllText(path);
        }

        [Test]
        public void GetStudentProfilePanelData_DefaultCorrespondent()
        {
           
            var result = _searchResultParser.ParseSearchResult(_searchResult);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0], 1);
        }
    }
}
