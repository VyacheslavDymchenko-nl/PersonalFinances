using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseItem>>> GetExpenseItem()
    {
        return await _context.ExpenseItems.ToListAsync();
    }

    // GET: api/ExpenseItem/5
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
    [HttpPost]
    public async Task<ActionResult<ExpenseItem>> PostExpenseItem(ExpenseItem expenseitem)
    {
        _context.ExpenseItems.Add(expenseitem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetExpenseItem", new { expenseitemid = expenseitem.ExpenseItemId }, expenseitem);
    }

    // DELETE: api/ExpenseItem/5
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
