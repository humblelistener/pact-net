using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookApiClient
{
    public class BookClient
    {
        public string BaseUri { get; set; }

        public BookClient(string baseUri = null)
        {
            BaseUri = baseUri ?? "http://book.readify.com/api";
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public IEnumerable<dynamic> GetBook()
        {
            var client = new HttpClient {BaseAddress = new Uri(BaseUri)};

            var request = new HttpRequestMessage(HttpMethod.Get, "/books");
            request.Headers.Add("Accept", "application/json");

            var response = client.SendAsync(request);

            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            if (status == HttpStatusCode.OK)
            {
                return !String.IsNullOrEmpty(content) ?
                    JsonConvert.DeserializeObject<IEnumerable<dynamic>>(content, _jsonSettings)
                    : new List<dynamic>();
            }

            throw new Exception("Server responded with a non 200 status code");
        }
    }
}
