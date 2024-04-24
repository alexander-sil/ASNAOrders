
using ASNAOrders.Web.Administration.Server.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ASNAOrders.Web.Administration.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CustomDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite("Data Source=App.db,Cache=Shared");
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
