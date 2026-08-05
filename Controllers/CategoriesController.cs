using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

/// <summary>
/// Управление категориями расходов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly FinanceContext _context;

    public CategoriesController(FinanceContext context)
    {
        _context = context;
    }


    // GET: api/Category
    /// <summary>
    /// Возвращает список всех категорий.
    /// </summary>
    /// <returns>Коллекция категорий.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
    {
        return await _context.Categories.ToListAsync();
    }

    // PUT: api/Category/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Изменяет категорию.
    /// </summary>
    /// <param name="categoryid">ID изменяемой категории.</param>
    /// <param name="category">Данные изменяемой категории.</param>
    /// <returns>BadRequest, ID категории не совпадают, NotFound если категория не найдена и NoContent, если все прошло успешно.</returns>
    [HttpPut("{categoryid}")]
    public async Task<IActionResult> PutCategory(System.Guid? categoryid, Category category)
    {
        if (categoryid != category.CategoryId)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(categoryid))
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

    // POST: api/Category
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Создает новую категорию.
    /// </summary>
    /// <param name="category">Данные новой категории.</param>
    /// <returns>Созданная категория.</returns>
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new { categoryid = category.CategoryId }, category);
    }


    // DELETE: api/Category/5
    /// <summary>
    /// Удаляет категорию по идентификатору.
    /// </summary>
    /// <param name="categoryid">Идентификатор категории.</param>
    /// <returns>NotFound если категория не найдена и NoContent, если все прошло успешно.</returns>
    [HttpDelete("{categoryid}")]
    public async Task<IActionResult> DeleteCategory(System.Guid? categoryid)
    {
        var category = await _context.Categories.FindAsync(categoryid);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(System.Guid? categoryid)
    {
        return _context.Categories.Any(e => e.CategoryId == categoryid);
    }

}
