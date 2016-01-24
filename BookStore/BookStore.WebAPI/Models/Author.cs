using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebAPI.Models
{
    public class Author
    {
        public Author()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}