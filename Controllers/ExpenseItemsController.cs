using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

/// <summary>
/// Управление статьями расходов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ExpenseItemsController : ControllerBase
{
    private readonly FinanceContext _context;
    public ExpenseItemsController(FinanceContext context)
    {
        _context = context;
    }

    // GET: api/ExpenseItem
    /// <summary>
    /// Возвращает список всех статей расходов.
    /// </summary>
    /// <returns>Коллекция статей расходов.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseItem>>> GetExpenseItem()
    {
        return await _context.ExpenseItems.ToListAsync();
    }

    // GET: api/ExpenseItem/5
    /// <summary>
    /// Возвращает статью расходов по ID.
    /// </summary>
    /// <param name="expenseitemid">ID нужной статьи расходов.</param>
    /// <returns>Нужная статья расходов, или NotFound если статья не найдена.</returns>
    [HttpGet("{expenseitemid}")]
    public async Task<ActionResult<ExpenseItem>> GetExpenseItem(System.Guid expenseitemid)
    {
        var expenseitem = await _context.ExpenseItems.FindAsync(expenseitemid);

        if (expenseitem == null)
        {
            return NotFound();
        }

        return expenseitem;
    }

    // PUT: api/ExpenseItem/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Изменяет статью расходов.
    /// </summary>
    /// <param name="expenseitemid">ID изменяемой статьи расходов.</param>
    /// <param name="expenseitem">Данные изменяемой статьи расходов.</param>
    /// <returns>BadRequest, ID статьи не совпадают, NotFound если статья не найдена и NoContent, если все прошло успешно.</returns>
    [HttpPut("{expenseitemid}")]
    public async Task<IActionResult> PutExpenseItem(System.Guid? expenseitemid, ExpenseItem expenseitem)
    {
        if (expenseitemid != expenseitem.ExpenseItemId)
        {
            return BadRequest();
        }

        _context.Entry(expenseitem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExpenseItemExists(expenseitemid))
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

    // POST: api/ExpenseItem
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Создает новую статью расходов.
    /// </summary>
    /// <param name="expenseitem">Данные новой статьи расходов.</param>
    /// <returns>Созданная статья расходов.</returns>
    [HttpPost]
    public async Task<ActionResult<ExpenseItem>> PostExpenseItem(ExpenseItem expenseitem)
    {
        _context.ExpenseItems.Add(expenseitem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetExpenseItem", new { expenseitemid = expenseitem.ExpenseItemId }, expenseitem);
    }

    // DELETE: api/ExpenseItem/5
    /// <summary>
    /// Удаляет статью расходов по идентификатору.
    /// </summary>
    /// <param name="expenseitemid">Идентификатор статьи расходов.</param>
    /// <returns>NotFound если статья не найдена и NoContent, если все прошло успешно.</returns>
    [HttpDelete("{expenseitemid}")]
    public async Task<IActionResult> DeleteExpenseItem(System.Guid? expenseitemid)
    {
        var expenseitem = await _context.ExpenseItems.FindAsync(expenseitemid);
        if (expenseitem == null)
        {
            return NotFound();
        }

        _context.ExpenseItems.Remove(expenseitem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExpenseItemExists(System.Guid? expenseitemid)
    {
        return _context.ExpenseItems.Any(e => e.ExpenseItemId == expenseitemid);
    }
}
