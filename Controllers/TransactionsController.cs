using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly FinanceContext _context;
    public TransactionsController(FinanceContext context)
    {
        _context = context;
    }

    // GET: api/Transaction
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
    {
        return await _context.Transactions.ToListAsync();
    }

    // GET: api/Transaction/5
    [HttpGet("{transactionid}")]
    public async Task<ActionResult<Transaction>> GetTransaction(System.Guid transactionid)
    {
        var transaction = await _context.Transactions.FindAsync(transactionid);

        if (transaction == null)
        {
            return NotFound();
        }

        return transaction;
    }

    // PUT: api/Transaction/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{transactionid}")]
    public async Task<IActionResult> PutTransaction(System.Guid? transactionid, Transaction transaction)
    {
        if (transactionid != transaction.TransactionId)
        {
            return BadRequest();
        }

        _context.Entry(transaction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransactionExists(transactionid))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Transaction
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTransaction", new { transactionid = transaction.TransactionId }, transaction);
    }

    // DELETE: api/Transaction/5
    [HttpDelete("{transactionid}")]
    public async Task<IActionResult> DeleteTransaction(System.Guid? transactionid)
    {
        var transaction = await _context.Transactions.FindAsync(transactionid);
        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(System.Guid? transactionid)
    {
        return _context.Transactions.Any(e => e.TransactionId == transactionid);
    }
}
