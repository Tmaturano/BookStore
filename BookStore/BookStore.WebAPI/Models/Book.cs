using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebAPI.Models
{
    public class Book
    {
        public Book()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Author Author { get; set; }
    }
}