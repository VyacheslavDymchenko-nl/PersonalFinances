using Microsoft.EntityFrameworkCore;
using PersonalFinances.Models;

namespace PersonalFinances
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("FinanceContext") ?? throw new InvalidOperationException("Connection string 'FinanceContext' not found.");

            builder.Services.AddDbContext<FinanceContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddControllers();

            WebApplication app = builder.Build();
            app.MapControllers();

            /*var categories = new List<Category>
            {
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Clothes", Budget = 37.34m, },
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Holidays", Budget = 48.24m },
                new() { CategoryId = Guid.NewGuid(), CategoryName = "Food", Budget = 56.35m }
            };*/

            /*app.Run(async (context) =>
            {
                HttpResponse response = context.Response;
                HttpRequest request = context.Request;
                PathString path = request.Path;
                //string expressionForNumber = "^/api/users/([0-9]+)$";   // если id представляет число

                // 2e752824-1657-4c7f-844b-6ec2e168e99c
                string expressionForGuid = @"^/api/categories/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
                if (path == "/api/categories" && request.Method == "GET")
                {
                    await GetAllCategories(response);
                }
                *//*else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
                {
                    // получаем id из адреса url
                    string? id = path.Value?.Split("/")[3];
                    await GetPerson(id, response);
                }
                else if (path == "/api/users" && request.Method == "POST")
                {
                    await CreatePerson(response, request);
                }
                else if (path == "/api/users" && request.Method == "PUT")
                {
                    await UpdatePerson(response, request);
                }
                else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
                {
                    string? id = path.Value?.Split("/")[3];
                    await DeletePerson(id, response);
                }*//*
                else
                {
                    *//*response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("html/index.html");*//*
                    await response.WriteAsync("Not good)");
                }
            });*/

            app.Run();

            /*async Task GetAllCategories(HttpResponse response)
            {

                await response.WriteAsJsonAsync(categories);
            }*/
        }


    }
}
