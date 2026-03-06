using Microsoft.EntityFrameworkCore;

namespace UserManagement
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
            // L?y chu?i k?t n?i t? file appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("MyCnn");

            // ??ng ký d?ch v? Database Context
            builder.Services.AddDbContext<UserManagement.Models.UserManagementDbContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

            app.MapControllerRoute(
        name: "default",
        pattern: "{controller=NguoiDung}/{action=Index}/{id?}");

            app.Run();
		}
	}
}
