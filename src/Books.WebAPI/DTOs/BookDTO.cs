﻿using System;

namespace Books.WebApi.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
