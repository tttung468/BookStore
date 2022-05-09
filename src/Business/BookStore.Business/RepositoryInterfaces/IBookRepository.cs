using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Business.Entities;

namespace BookStore.Business.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<IList<Book>> GetBooks();
        Task<Book?> GetBookById(int id);
        Task<Book> InsertBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(Book book);
    }
}