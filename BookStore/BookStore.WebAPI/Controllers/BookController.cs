using BookStore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.WebAPI.Controllers
{
    [RoutePrefix("api/Books")]
    public class BookController : ApiController
    {
        private IList<Book> _books;
        public BookController()
        {
            _books = new List<Book>();

            _books.Add(new Book()
            {
                Code = 111,
                Description = "My Little House",
                Price = 19.99,
                Author = new Author() { Code = 1, Name = "John Yellow" }
            });

            _books.Add(new Book()
            {
                Code = 222,
                Description = "Success in Vancouver!",
                Price = 39.50,
                Author = new Author() { Code = 2, Name = "Hamilton Blue" }
            });

            _books.Add(new Book()
            {
                Code = 333,
                Description = "50 things to do before you die.",
                Price = 19.99,
                Author = new Author() { Code = 3, Name = "Fisherman Red" }
            });
        }

        // GET: api/Book
        //[Route("GetBooks/{bookId}")] parameter 
        [Route("GetBooks")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get()
        {
            var books = _books.ToList();
            
            return Request.CreateResponse(HttpStatusCode.OK, books);
        }

        // GET: api/Book/5
        [Route("GetBook/{code}")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get(int code)
        {            
            var book = _books.ToList().Where(b => b.Code == code).FirstOrDefault();

            if (book == null)
                return Request.CreateResponse(HttpStatusCode.BadGateway);
            else
                return Request.CreateResponse(HttpStatusCode.OK, book);
        }

        // POST: api/Book
        [HttpPost]
        [Route("AddBook")]        
        public HttpResponseMessage Post(Book book)
        {
            if (book == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest); // if you want a specific error object, just create a class and return here.

            try
            {
                _books.Add(book);                

                return Request.CreateResponse(HttpStatusCode.OK, _books.ToList()); //returning all the books
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error to add a new Book in the List");
                
            }
        }

        // PUT: api/Book/5
        [HttpPost]
        [Route("UpdateBook")]
        public HttpResponseMessage Put(Book book)
        {
            if (book == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            //if (!_books.Contains(book))
            if(!_books.Any(b => b.Code == book.Code))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                //update the given book
                var bookToBeUpdated = _books.Where(b => b.Code== book.Code).FirstOrDefault();
                _books.Remove(bookToBeUpdated);
                _books.Add(book);

                return Request.CreateResponse(HttpStatusCode.OK, _books.ToList());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error to update the given book in the List");                
            }
        }

        // DELETE: api/Book/5
        [HttpPost]
        [Route("DeleteBook/{code:int}")]
        public HttpResponseMessage Delete(int code)
        {            
            var bookToBeDeleted = _books.Where(b => b.Code == code).FirstOrDefault();

            if (bookToBeDeleted == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                _books.Remove(bookToBeDeleted);

                return Request.CreateResponse(HttpStatusCode.OK, _books.ToList());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    string.Format("Error when trying to delete the book {0}", bookToBeDeleted.Description));                
            }
        }
    }
}
