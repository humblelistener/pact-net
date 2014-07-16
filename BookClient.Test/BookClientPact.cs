using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace BookClient.Test
{
    public class BookClientPact : IDisposable
    {
        public IPactConsumer Pact { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 4542; } }

        public string MockServerBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

        public BookClientPact()
        {
            Pact = new Pact().ServiceConsumer("reader")
                .HasPactWith("book api");

            MockProviderService = Pact.MockService(MockServerPort);
        }

        public void Dispose()
        {
            Pact.Dispose();
        }
    }
}
