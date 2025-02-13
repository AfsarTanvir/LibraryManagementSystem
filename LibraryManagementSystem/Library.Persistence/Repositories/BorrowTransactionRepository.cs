using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence.Repositories
{
    public class BorrowTransactionRepository : IBorrowTransactionRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowTransactionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowTransaction>> GetAllAsync()
        {
            return await _context.BorrowTransactions
                                 .Include(bt => bt.Book)
                                 .Include(bt => bt.Member)
                                 .ToListAsync();
        }

        public async Task<BorrowTransaction> GetByIdAsync(int id)
        {
            return await _context.BorrowTransactions
                                 .Include(bt => bt.Book)
                                 .Include(bt => bt.Member)
                                 .FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task AddAsync(BorrowTransaction transaction)
        {
            await _context.BorrowTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowTransaction transaction)
        {
            _context.BorrowTransactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _context.BorrowTransactions.FindAsync(id);
            if (transaction != null)
            {
                _context.BorrowTransactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BorrowTransaction> BorrowBookAsync(int bookId, int memberId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || !book.IsAvailable)
            {
                throw new Exception("Book is not available for borrowing.");
            }

            var transaction = new BorrowTransaction
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            book.IsAvailable = false;
            _context.BorrowTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<bool> ReturnBookAsync(int transactionId)
        {
            var transaction = await _context.BorrowTransactions
                                            .Include(bt => bt.Book)
                                            .FirstOrDefaultAsync(bt => bt.Id == transactionId);

            if (transaction == null || transaction.ReturnedDate != null)
            {
                return false;
            }

            transaction.ReturnedDate = DateTime.Now;
            transaction.Book.IsAvailable = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
