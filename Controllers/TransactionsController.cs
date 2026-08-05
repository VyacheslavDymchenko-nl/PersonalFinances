using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

/// <summary>
/// Управление транзакциями.
/// </summary>
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
    /// <summary>
    /// Возвращает список всех транзакций.
    /// </summary>
    /// <returns>Коллекция транзакций.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction()
    {
        return await _context.Transactions.ToListAsync();
    }

    // GET: api/Transaction/5
    /// <summary>
    /// Возвращает транзакцию по ID.
    /// </summary>
    /// <param name="transactionid">ID нужной транзакции.</param>
    /// <returns>Нужная транзакция, или NotFound если транзакция не найдена.</returns>
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
    /// <summary>
    /// Изменяет транзакцию.
    /// </summary>
    /// <param name="transactionid">ID изменяемой транзакции.</param>
    /// <param name="transaction">Данные изменяемой транзакции.</param>
    /// <returns>BadRequest, ID транзакции не совпадают, NotFound если транзакция не найдена и NoContent, если все прошло успешно.</returns>
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
    /// <summary>
    /// Создает новую транзакцию.
    /// </summary>
    /// <param name="transaction">Данные новой транзакции.</param>
    /// <returns>Созданная транзакция.</returns>
    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTransaction", new { transactionid = transaction.TransactionId }, transaction);
    }

    // DELETE: api/Transaction/5
    /// <summary>
    /// Удаляет транзакцию по идентификатору.
    /// </summary>
    /// <param name="transactionid">Идентификатор транзакции</param>
    /// <returns>NotFound если транзакция не найдена и NoContent, если все прошло успешно.</returns>
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

    /// <summary>
    /// Возвращает список транзакций за определенный день.
    /// </summary>
    /// <param name="date">Дата.</param>
    /// <returns>Коллекция транзакций.</returns>
    [HttpGet("day/{date}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByDay(DateOnly date)
    {
        return await _context.Transactions
            .Where(transaction => transaction.TransactionDate == date)
            .ToListAsync();
    }

    /// <summary>
    /// Возвращает список транзакций за определенный месяц.
    /// </summary>
    /// <param name="year">Год.</param>
    /// <param name="month">Месяц.</param>
    /// <returns>Коллекция транзакций.</returns>
    [HttpGet("month")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByMonth(
    int year,
    int month)
    {
        return await _context.Transactions
            .Where(transaction =>
                transaction.TransactionDate.Year == year &&
                transaction.TransactionDate.Month == month)
            .ToListAsync();
    }
}
