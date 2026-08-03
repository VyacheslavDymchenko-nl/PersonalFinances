using System;
using System.Collections.Generic;

namespace PersonalFinances.Models;

public partial class ExpenseItem
{
    public Guid ExpenseItemId { get; set; }

    public string ExpenseItemName { get; set; } = null!;

    public Guid? CategoryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
