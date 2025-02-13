using Library.Application;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}