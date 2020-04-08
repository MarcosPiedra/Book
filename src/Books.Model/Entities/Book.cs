using System;

namespace Books.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsRead { get; set; }
    }
}