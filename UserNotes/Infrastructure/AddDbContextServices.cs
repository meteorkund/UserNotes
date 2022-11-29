using Microsoft.EntityFrameworkCore;

namespace UserNotes.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<UserNotesContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        }
    }
}
