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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Vue", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Vue");
            app.MapControllers();
            app.Run();
        }


    }
}
