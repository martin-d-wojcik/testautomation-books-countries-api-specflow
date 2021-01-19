using RestApi.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestApi.RestApis
{
    class RestApisBooks
    {
        public static string baseUrl = "https://www.booknomads.com/api/v0";
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ISBN{ get; set; }
        public string LanguageCode { get; set; }
        public List<Author> Authors { get; set; }
        public string Description { get; set; }
        public string CoverThumb { get; set; }
        public List<string> Subjects { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }
}
