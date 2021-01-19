using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using RestApi.RestApis;
using Newtonsoft.Json;
using RestApi.Helpers;
using Newtonsoft.Json.Linq;

namespace RestApi.Steps
{
    [Binding]
    public class StepsCountries
    {
        [Given(@"I have the endpoint ""(.*)""")]
        public void GivenIHaveTheEndpoint(string endpoint)
        {
            RestApiHelper.SetUrl(RestApisCountries.baseUrl, endpoint);
        }

        [When(@"I perform a get call")]
        public void WhenIPerformAGetCall()
        {
            RestApiHelper.GetRequest();
        }

        [Then(@"the result contains more than one country")]
        public void ThenTheResultContainsMoreThanOneCountry()
        {
            var response = RestApiHelper.GetResponse();
            Array itemsInResponse = RestApiHelper.ParseResponseContentToArray(response);

            //Assert length of array of countries in response
            Assert.Greater(itemsInResponse.Length, 1);
            
            //Assert status code response
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }       

        [Then(@"the result contains the capital ""(.*)""")]
        public void ThenTheResultContainsTheCapital(string capitalName)
        {
            var response = RestApiHelper.GetResponse();
            Boolean capitalExists = RestApiHelper.SearchForValueInCountriesArrayFromResponse(response, capitalName);

            //Assert status code response
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);

            //Assert that the SearchForValueInArrayFromResponse() method returned a true value
            Assert.True(capitalExists);            
        }

        [When(@"I add a new country")]
        public void WhenIAddANewCountry()
        {
            string body = JsonHelper.CreateJsonNewCountry();            
            RestApiHelper.PostRequest(body);
        }

        [Then(@"Status code ""(.*)"" is returned")]
        public void ThenStatusCodeIsReturned(int expectedStatusCode)
        {
            var response = RestApiHelper.GetResponse();

            //Assert status code response
            System.Net.HttpStatusCode actualStatusCode = RestApiHelper.GetStatusCodeFromResponse(expectedStatusCode);            
            Assert.AreEqual(response.StatusCode, actualStatusCode);
        }

        [Then(@"Error message ""(.*)"" is returned")]
        public void ThenErrorMessageIsReturned(string expectedErrorMessage)
        {
            var response = RestApiHelper.GetResponse();
            string actualErrorMessage = RestApiHelper.GetErrorMessageFromResponse(response);

            //Assert the error message from the response
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage); //, MessageHelper.errorMessageNotReturned);
        }        
    }    
}
