using System.Collections.Generic;
using BookStore.Business.Models;
using System.Threading.Tasks;

namespace BookStore.Business.ServiceInterfaces
{
    public interface IBookService
    {
        Task<IList<BookModel>> GetBooksAsync();
        Task<BookModel?> GetBookByIdAsync(int id);
        Task<BookModel> InsertBookAsync(BookModel bookModel);
        Task<BookModel> UpdateBookAsync(BookModel bookModel);
        Task<bool> DeleteBookAsync(BookModel bookModel);
    }
}