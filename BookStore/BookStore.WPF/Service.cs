using BookStore.WebAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WPF
{
    public class Service
    {
        private IRestClient _client;
        
        public Service()
        {
            _client = new RestClient("http://localhost:51378/api");            
            
        }

        public BookBinding GetBookByCode(int code)
        {
            BookBinding book = new BookBinding();

            var request = new RestRequest("Books/GetBook/{code}", Method.GET);
            //request.AddParameter("code", code);
            request.AddUrlSegment("code", code.ToString());
            request.RequestFormat = DataFormat.Json;

            //var test = _client.ExecuteAsync<BookBinding>(request, r =>
            //{
            //    if (r.StatusCode == HttpStatusCode.OK)
            //    {
            //        book = r.Data;
            //    }
            //});



            //Sync
            IRestResponse<BookBinding> response = _client.Execute<BookBinding>(request);

            if (response.StatusCode == HttpStatusCode.OK)
                book = response.Data;

            return book;
        }

        public IRestResponse AddBook(BookBinding book)
        {
            var request = new RestRequest("Books/AddBook", Method.POST);            
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(book);
                        
            IRestResponse<List<BookBinding>> response = _client.Execute<List<BookBinding>>(request);
            
            return response;
        }

        public IRestResponse UpdateBook(BookBinding book)
        {
            var request = new RestRequest("Books/UpdateBook", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(book);

            IRestResponse<List<BookBinding>> response = _client.Execute<List<BookBinding>>(request);

            return response;
        }

        public IRestResponse DeleteBook(int code)
        {
            var request = new RestRequest("Books/DeleteBook/{code}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("code", code.ToString());

            IRestResponse<List<BookBinding>> response = _client.Execute<List<BookBinding>>(request);

            return response;
        }
    }
}
