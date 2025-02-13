using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowTransactionController : ControllerBase
    {
        private readonly IBorrowTransactionService _borrowTransactionService;

        public BorrowTransactionController(IBorrowTransactionService borrowTransactionService)
        {
            _borrowTransactionService = borrowTransactionService;
        }

        // GET: api/BorrowTransaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowTransaction>>> GetAllTransactions()
        {
            var transactions = await _borrowTransactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        // GET: api/BorrowTransaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowTransaction>> GetTransactionById(int id)
        {
            var transaction = await _borrowTransactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // POST: api/BorrowTransaction/borrow
        [HttpPost("borrow")]
        public async Task<ActionResult<BorrowTransaction>> BorrowBook(int bookId, int memberId)
        {
            var transaction = await _borrowTransactionService.BorrowBookAsync(bookId, memberId);

            if (transaction == null)
            {
                return BadRequest("Unable to borrow the book.");
            }

            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        // POST: api/BorrowTransaction/return
        [HttpPost("return/{transactionId}")]
        public async Task<ActionResult<bool>> ReturnBook(int transactionId)
        {
            var result = await _borrowTransactionService.ReturnBookAsync(transactionId);

            if (!result)
            {
                return BadRequest("Unable to return the book.");
            }

            return Ok("Book returned successfully.");
        }
    }
}
