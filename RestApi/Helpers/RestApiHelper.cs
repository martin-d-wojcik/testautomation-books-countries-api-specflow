using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApi.RestApis;
using RestSharp;

namespace RestApi.Helpers
{
    class RestApiHelper
    {
        public static RestClient client;
        public static RestRequest restRequest;
        private static string responseContent;

        public static RestClient SetUrl(string baseUrl, string endpoint)
        {
            //var url = Path.Combine(baseUrl, "/", endpoint);
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/");
            stringBuilder.Append(endpoint);
            Uri url = new Uri(stringBuilder.ToString());

            return client = new RestClient(url);
        }

        private static RestRequest SetHeaders()
        {
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static RestRequest PostRequest(string jsonString)
        {
            restRequest = new RestRequest(Method.POST);
            //restRequest.AddHeader("Content-Type", "application/json");
            SetHeaders();
            //specify the payload
            restRequest.AddParameter("application / json", jsonString, ParameterType.RequestBody);
            return restRequest;
        }

        public static RestRequest GetRequest()
        {
            restRequest = new RestRequest(Method.GET);
            //restRequest.AddHeader("Content-Type", "application/json");
            SetHeaders();
            return restRequest;
        }

        public static IRestResponse GetResponse()
        {
            return client.Execute(restRequest);
        }

        //search for at value in a object of specific class
        public static string SearchForValueInObject(IRestResponse response, string restApiName, string searchValue)
        {
            RestApisBooks booksObject;
            string returnValue = null;
            responseContent = response.Content;

            //TODO: create method for impl. below
            switch(restApiName)
            {
                case "RestApisBooks":
                    booksObject = JsonConvert.DeserializeObject<RestApisBooks>(responseContent);
                    //returnValue = booksObject.Authors[0].Name;
                    returnValue = booksObject.Title;
                    break;
            }
            
            return returnValue;
        }

        //gets the error message from an object of the RestApisCountriesErrorMessage class in case of status code: 405, 400..
        public static string GetErrorMessageFromResponse(IRestResponse response)
        {
            responseContent = response.Content;
            var errorMessageObject = JsonConvert.DeserializeObject<RestApisCountriesErrorMessage>(responseContent);

            return errorMessageObject.message;
        }

        //parses the result into a array containing objects of the Countries class and looks for the capital
        public static Boolean SearchForValueInCountriesArrayFromResponse(IRestResponse response, string searchValue)
        {
            Boolean returnValue = false;
            responseContent = response.Content;
            var itemsInResponse = JsonConvert.DeserializeObject<RestApisCountries[]>(responseContent);
            
            foreach(RestApisCountries country in itemsInResponse)
            {
                if(country.capital.Equals(searchValue))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        //return a array with the getters of the Countries class
        public static Array ParseResponseContentToArray(IRestResponse response)        
        {
            responseContent = response.Content;
            var itemsInResponse = JsonConvert.DeserializeObject<RestApisCountries[]>(responseContent);

            return itemsInResponse;
        }

        public static RestApisCountries ParseResponseContentToCountriesObject(IRestResponse response)
        {
            responseContent = response.Content;
            RestApisCountries itemsInResponse = JsonConvert.DeserializeObject<RestApisCountries>(responseContent);

            return itemsInResponse;
        }

        //doeasn't really work. Instead, use: SearchForValueInObject - method
        public static string getValueFromResponseContent(IRestResponse response, string key)
        {            
            string responseContent = response.Content;            
            dynamic api = JObject.Parse(responseContent);
            string completeKey = api + "." + key;
            string returnValue = completeKey;

            return api.key;

            /*
            var language = api.LanguageCode;
            var title = api.Title;
            var author = api.Authors[0].Name;
            */
        }  
        
        public static System.Net.HttpStatusCode GetStatusCodeFromResponse(int statusCode)
        {
            System.Net.HttpStatusCode statusCodeReturned = System.Net.HttpStatusCode.NotImplemented;

            switch(statusCode)
            {
                case 405:
                    statusCodeReturned = System.Net.HttpStatusCode.MethodNotAllowed;
                    break;
                case 200:
                    statusCodeReturned = System.Net.HttpStatusCode.OK;
                    break;
            }

            return statusCodeReturned;
        }
    }
}
