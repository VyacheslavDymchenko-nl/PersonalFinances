using System;
using System.Collections.Generic;

namespace PersonalFinances.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid? ExpenseItemId { get; set; }

    public DateOnly TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string? Comment { get; set; }

    public virtual ExpenseItem? ExpenseItem { get; set; }
}
