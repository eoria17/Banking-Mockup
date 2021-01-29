using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace McbaAdmin
{
    public class Helper
    {
        public static class MCBAWebApi
        {
            private const string ApiBaseUri = "http://localhost:5000";

            public static HttpClient InitializeClient()
            {
                var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }
        }
    }
}
