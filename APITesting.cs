using API_Testing_March2021.MODEL;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;

namespace API_Testing_March2021
{
    public class Tests
    {


        [TestCase("Julie")]
       
        public void FluentAssertion(string firstName)
        {

            BillingOrder expectedData = new BillingOrder(); //request

            BillingOrder actualData = new BillingOrder(firstName: "Elijah", city: "Cebu"); //response
           

            //assertion failed (Style 1)

            //Assert.Multiple(() =>
            //{

            //    Assert.AreEqual(expectedData.firstName, actualData.firstName);
            //    Assert.AreEqual(expectedData.lastName, actualData.lastName);

            //});

            expectedData.Should().BeEquivalentTo(actualData);


        }



        [TestCase ("Test")]
        public void TestCase_POST(string fname)



        {
            //string postBody = "{\r\n  \"addressLine1\": \"Cockayne\",\r\n  \"addressLine2\": \"Sunnynook\",\r\n  \"city\": \"string\",\r\n  \"comment\": \"string\",\r\n  \"email\": \"julia@gmail.com\",\r\n  \"firstName\": \"Julia Abigail\",\r\n  \"id\": 0,\r\n  \"itemNumber\": 0,\r\n  \"lastName\": \"Mondero\",\r\n  \"phone\": \"0210848080\",\r\n  \"state\": \"AL\",\r\n  \"zipCode\": \"061102\"\r\n}\r\n\r\n";

            /*
             
             string abc = null;
            TestContext.WriteLine(abc ?? "123");
            abc = "111";
            estContext.WriteLine(abc ?? "123");
             
             */

            //Changing the name to new one, but will call all the default value
            // BillingOrder expectedData = new BillingOrder(firstName: "Julie");


            //creating data object

            BillingOrder expectedBillingOrder = new BillingOrder();

            //CONVERT ORDER TO JSON
            var jsonBody = JsonConvert.SerializeObject(expectedBillingOrder);

            //posting data - creating -----API
            IRestResponse response = POST(jsonBody);

            TestContext.WriteLine(response.Content);

            //Deserialize
            //converting response content to object

            BillingOrder actualBillingOrder  = JsonConvert.DeserializeObject<BillingOrder>(response.Content);

            //hack
            // expectedData.id = actualData.id;

            //TestContext.WriteLine(actualData.firstName);

            expectedBillingOrder.Should().BeEquivalentTo(actualBillingOrder,  options => options.Excluding(o => o.id));

        }

        //API METHODS

        string baseURL = "http://localhost:8080/";


        [Test]
        public IRestResponse POST(string body)
        {

            //creating the client request connection
            var client = new RestClient($"{baseURL}/BillingOrder");


            //creating request such as POST/GET/DEL
            var request = new RestRequest(Method.POST);

            //adding the header (REQUEST PARAMETER)
            request.AddHeader("Content-Type", "application/json");

            //Adding the Body
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //executing the request
            IRestResponse response = client.Execute(request);

            return response;
        }


        [Test]
        public IRestResponse PUT(int id, string body)
        {

            //creating the client request connection
            var client = new RestClient($"{baseURL}/BillingOrder/{id}");


            //creating request such as POST/GET/DEL
            var request = new RestRequest(Method.PUT);

            //adding the header (REQUEST PARAMETER)
            request.AddHeader("Content-Type", "application/json");

            //Adding the Body
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            //executing the request
            IRestResponse response = client.Execute(request);

            return response;
        }



        [Test]
        public IRestResponse GETSINGLE(int id)
        {

            //creating the client request connection
            var client = new RestClient($"{baseURL}/BillingOrder/{id}");


            //creating request such as POST/GET/DEL
            var request = new RestRequest(Method.GET);

            //executing the request
            IRestResponse response = client.Execute(request);

            return response;
        }


        [Test]
        public IRestResponse GETALL()
        {

            //creating the client request connection
            var client = new RestClient($"{baseURL}/BillingOrder");


            //creating request such as POST/GET/DEL
            var request = new RestRequest(Method.GET);

            //executing the request
            IRestResponse response = client.Execute(request);

            return response;
        }


        [Test]
        public IRestResponse DELETESINGLE(int id)
        {

            //creating the client request connection
            var client = new RestClient($"{baseURL}/BillingOrder/{id}");


            //creating request such as POST/GET/DEL
            var request = new RestRequest(Method.DELETE);

            //executing the request
            IRestResponse response = client.Execute(request);

            TestContext.WriteLine(response.StatusCode);

            return response;


        }

        //[Test]
        //public IRestResponse DELETEALL()
        //{

        //    for (int i = 0; i <= 50; i++)


        //    {


        //        //creating the client request connection
        //        var client = new RestClient($"{baseURL}/BillingOrder/{i}");


        //        //creating request such as POST/GET/DEL
        //        var request = new RestRequest(Method.DELETE);

        //        //executing the request
        //        IRestResponse response = client.Execute(request);
        //        TestContext.WriteLine(response.StatusCode);

        //        return response;

        //    }
        //}



    }
}
