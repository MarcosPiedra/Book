using System;

namespace Books.Model.Entities
{
    public class BookEntity : Entity<int?>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsRead { get; set; }
    }
}