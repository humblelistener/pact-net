using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PactNet.Mocks.MockHttpService.Models;
using System.Linq;

namespace BookClient.Test
{
    [TestClass]
    public class BookClientTests
    {
        private BookClientPact pact;

        [TestInitialize]
        public void Setup()
        {
            pact = new BookClientPact();
        }

        [TestMethod]
        public void GetBooks_WhenCalled_RetunsAllBook()
        {
            // arrange
            // in a usual test scenario, we will be mocking the service here.
            pact.MockProviderService.Given("there are books with ISBN 'abcde' and 'efghi'")
                .UponReceiving("A GET request to retreive all the books")
                .With(new PactProviderServiceRequest()
                {
                    Method = HttpVerb.Get,
                    Path = "/books",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new PactProviderServiceResponse
                {
                    Status = 200,
                     Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new []
                    {
                        new {ISBN = "abcde", Author = "k", Name = "m"},
                        new {ISBN = "efghi", Author = "k", Name = "m"}
                    }
                })
                .RegisterInteraction();

            var client = new ApiClient(pact.MockServerBaseUri);

            //Act
            var books = client.GetBooks();

            //Assert
            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count());
        }
    }
}
