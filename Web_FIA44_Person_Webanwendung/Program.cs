namespace Web_FIA44_Person_Webanwendung
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();
			var app = builder.Build();

			app.MapControllerRoute(name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.UseStaticFiles();

			app.UseRouting();
			app.Run();
		}
	}
}
