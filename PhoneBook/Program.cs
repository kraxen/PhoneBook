using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ����� ������������
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<DbPhoneBookModel>(options => options.UseSqlServer(connection));

            //builder.Services.AddSingleton<IPhoneBookModel, DefaultPhoneBookModel>();
            builder.Services.AddScoped<IPhoneBookModel, DbPhoneBookModel>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/PhoneBook/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=PhoneBook}/{action=Index}");


            app.Run();
        }
    }
}