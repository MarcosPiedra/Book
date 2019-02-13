using Books.Data.EntityFramework.Context;
using Books.Model.Entities;

namespace Books.Repository
{
    public class BooksRepository : RepositoryBase<BookEntity>, IBooksRepository
    {
        public BooksRepository(BooksContext context): base(context)
        {
        }
    }
}
