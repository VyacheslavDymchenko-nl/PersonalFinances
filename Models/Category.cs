namespace PersonalFinances.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public decimal Budget { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; } = new List<ExpenseItem>();
}
