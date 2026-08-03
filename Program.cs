using PersonalFinances.Models;

namespace PersonalFinances
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            var users = new List<Category>
            {
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Clothes", Budget = 37.34m, },
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Holidays", Budget = 48.24m },
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Food", Budget = 56.35m }
            };

            app.Run();


        }
    }
}
