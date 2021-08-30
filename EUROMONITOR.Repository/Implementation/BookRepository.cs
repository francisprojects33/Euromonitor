using EUROMONITOR.Model;
using EUROMONITOR.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EUROMONITOR.Repository.Implementation
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {

        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Book> GetBookAsync(Guid bookId, bool trackChanges) =>
            await FindByCondition(c => c.BookId.Equals(bookId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateBook(Book book) => Create(book);

        public void DeleteBook(Book book)
        {
            Delete(book);
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }
    }
}
