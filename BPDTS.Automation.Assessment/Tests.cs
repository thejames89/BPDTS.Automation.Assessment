using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;

namespace BPDTS.Automation.Assessment
{
    [TestFixture]
    public class APITests
    {
        [Test]
        public void GetInstructions()
        {
            RestClient restClient = new RestClient(TestData.Uri);
            RestRequest restRequest = new RestRequest($"/instructions", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            var status = restResponse.StatusDescription;
            var jsonResponse = JObject.Parse(restResponse.Content);
            var todo= jsonResponse.GetValue("todo").ToString();
            Assert.That(status == "OK");
            Assert.That(todo == TestData.Instructions);
        }
        [Test]
        public void GetCity()
        {
            RestClient restClient = new RestClient(TestData.Uri);
            RestRequest restRequest = new RestRequest($"/city/{TestData.City}/users", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            var status = restResponse.StatusDescription;
            var content = restResponse.Content;
            Assert.That(status == "OK");
            Assert.That(content.Contains(TestData.Firstname));
        }
        [Test]
        public void GetAllUsers()
        {
            RestClient restClient = new RestClient(TestData.Uri);
            RestRequest restRequest = new RestRequest($"/users", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            var jsonResponse = JArray.Parse(restResponse.Content);
            var entries = jsonResponse.Count;
            var status = restResponse.StatusDescription;
            var contentLength = restResponse.ContentLength.ToString();
            Assert.That(status == "OK");
            Assert.That(contentLength == "175719");
            Assert.That(entries == 1000);
        }
        [Test]
        public void GetValidUser()
        { 
            RestClient restClient = new RestClient(TestData.Uri);
            RestRequest restRequest = new RestRequest($"/user/{TestData.Id}", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            var status = restResponse.StatusDescription;
            var jsonResponse = JObject.Parse(restResponse.Content);
            var firstname = jsonResponse.GetValue("first_name").ToString();
            var id = jsonResponse.GetValue("id").ToString();
            Assert.That(status == "OK");
            Assert.That(firstname == TestData.Firstname);
            Assert.That(id == TestData.Id);
        }
        [Test]
        public void GetInvalidUser()
        {
            RestClient restClient = new RestClient(TestData.Uri);
            RestRequest restRequest = new RestRequest($"/user/1001", Method.GET);
            IRestResponse restResponse = restClient.Execute(restRequest);
            var status = restResponse.StatusDescription;
            string response = restResponse.Content;
            Assert.That(status == "NOT FOUND");
        }
    }
}