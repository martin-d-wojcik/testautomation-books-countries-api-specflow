using NUnit.Framework;
using RestApi.Helpers;
using System;
using TechTalk.SpecFlow;
using RestApi.RestApis;

namespace RestApi
{
    [Binding]
    public class StepsBooks
    {
        [Given(@"I have the endpoint ""(.*)"" to the books api")]
        public void GivenIHaveTheEndpointToTheBooksApi(string endpoint)
        {
            RestApiHelper.SetUrl(RestApisBooks.baseUrl, endpoint);
        }

        [When(@"I add a new book")]
        public void WhenIAddANewBook()
        {
            string body = JsonHelper.CreateJsonNewBook();
            RestApiHelper.PostRequest(body);
        }

        [Then(@"the result contains the title ""(.*)""")]
        public void ThenTheResultContainsTheTitle(string expectedTitle)
        {
            var response = RestApiHelper.GetResponse();
            //var title = RestApiHelper.getValueFromResponseContent(response, "Title");
            var title = RestApiHelper.SearchForValueInObject(response, "RestApisBooks", "Title");

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            //Assert that the response contains the title of the book 
            Assert.AreEqual(expectedTitle, title);
        }
    }
}
