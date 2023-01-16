using CompanyWebcast.Domain.WeatherForecast;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebcast.Infrastructure.Persistance
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            base.OnModelCreating(builder);
        }

    }
}
