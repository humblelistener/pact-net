using System.Collections.Generic;
using System.Web.Http;

namespace BooksApi.Controllers
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        [Route("")]
        public IEnumerable<Book> Get()
        {
            return new List<Book>
            {
                new Book {ISBN = "asdfads", Author = "k", Name = "m"},
                new Book {ISBN = "asf", Author = "k", Name = "m"}
            };
        }

        [Route("{id:int}")]
        public Book Get(int id)
        {
            return new Book {ISBN = "asdfads", Author = "k", Name = "m"};
        }
    }
}
