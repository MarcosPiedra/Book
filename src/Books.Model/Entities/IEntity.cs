using System;

namespace Books.Model.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}

