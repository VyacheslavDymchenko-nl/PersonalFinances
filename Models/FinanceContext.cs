using Microsoft.EntityFrameworkCore;

namespace PersonalFinances.Models;

public partial class FinanceContext : DbContext
{
    public FinanceContext()
    {
    }

    public FinanceContext(DbContextOptions<FinanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ExpenseItem> ExpenseItems { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PersonalFinances;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B275E63F5");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E090662DF2").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Budget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ExpenseItem>(entity =>
        {
            entity.HasKey(e => e.ExpenseItemId).HasName("PK__ExpenseI__E41A54D4A7A586C9");

            entity.Property(e => e.ExpenseItemId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ExpenseItemID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ExpenseItemName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.ExpenseItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__ExpenseIt__Categ__6E01572D");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B181226EF");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("Tr_Transactions_AfterInsertUpdate_CheckDailyAmount");
                    tb.HasTrigger("Tr_Transactions_AfterInsertUpdate_CheckExpenseItemForActivity");
                    tb.HasTrigger("Tr_Transactions_AfterUpdate_BlockExpenseItemChange");
                });

            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Comment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExpenseItemId).HasColumnName("ExpenseItemID");

            entity.HasOne(d => d.ExpenseItem).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ExpenseItemId)
                .HasConstraintName("FK__Transacti__Expen__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
