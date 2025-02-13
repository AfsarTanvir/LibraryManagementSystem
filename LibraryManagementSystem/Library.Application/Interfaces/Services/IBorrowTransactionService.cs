using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Services
{
    public interface IBorrowTransactionService
    {
        Task<IEnumerable<BorrowTransaction>> GetAllTransactionsAsync();
        Task<BorrowTransaction> GetTransactionByIdAsync(int id);
        Task<BorrowTransaction> BorrowBookAsync(int bookId, int memberId);
        Task<bool> ReturnBookAsync(int transactionId);
    }
}
