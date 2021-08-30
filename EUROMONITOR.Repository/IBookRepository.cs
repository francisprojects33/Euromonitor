using EUROMONITOR.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUROMONITOR.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
        Task<Book> GetBookAsync(Guid bookId, bool trackChanges);
        void CreateBook(Book book);
        void DeleteBook(Book book);
        void UpdateBook(Book book);
    }
}
