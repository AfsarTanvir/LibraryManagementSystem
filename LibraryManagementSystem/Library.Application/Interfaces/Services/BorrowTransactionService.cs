using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class BorrowTransactionService : IBorrowTransactionService
    {
        private readonly IBorrowTransactionRepository _borrowTransactionRepository;

        public BorrowTransactionService(IBorrowTransactionRepository borrowTransactionRepository)
        {
            _borrowTransactionRepository = borrowTransactionRepository;
        }

        public async Task<IEnumerable<BorrowTransaction>> GetAllTransactionsAsync()
        {
            return await _borrowTransactionRepository.GetAllAsync();
        }

        public async Task<BorrowTransaction> GetTransactionByIdAsync(int id)
        {
            return await _borrowTransactionRepository.GetByIdAsync(id);
        }

        public async Task<BorrowTransaction> BorrowBookAsync(int bookId, int memberId)
        {
            return await _borrowTransactionRepository.BorrowBookAsync(bookId, memberId);
        }

        public async Task<bool> ReturnBookAsync(int transactionId)
        {
            return await _borrowTransactionRepository.ReturnBookAsync(transactionId);
        }
    }
}
