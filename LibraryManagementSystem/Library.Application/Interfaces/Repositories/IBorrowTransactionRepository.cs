using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBorrowTransactionRepository
    {
        Task<IEnumerable<BorrowTransaction>> GetAllAsync();
        Task<BorrowTransaction> GetByIdAsync(int id);
        Task AddAsync(BorrowTransaction transaction);
        Task UpdateAsync(BorrowTransaction transaction);
        Task DeleteAsync(int id);

        Task<BorrowTransaction> BorrowBookAsync(int bookId, int memberId);
        Task<bool> ReturnBookAsync(int transactionId);
    }
}