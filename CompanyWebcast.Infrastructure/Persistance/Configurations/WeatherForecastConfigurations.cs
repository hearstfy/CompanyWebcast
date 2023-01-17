using CompanyWebcast.Domain.WeatherForecast;
using CompanyWebcast.Domain.WeatherForecast.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyWebcast.Infrastructure.Persistance.Configurations
{
    public class WeatherForecastConfigurations : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            ConfigureWeatherForecastTable(builder);
            ConfigureWeatherForecastHourlyTable(builder);
        }

        private void ConfigureWeatherForecastHourlyTable(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.OwnsMany(wf => wf.HourlyForecasts, hfb =>
            {
                hfb.ToTable("WeatherForecastHourly");
                hfb.WithOwner().HasForeignKey("WeatherForecastId");
                hfb.HasKey("Id", "WeatherForecastId");
                hfb.HasIndex("WeatherForecastId","StartHour", "EndHour").IsUnique();
                hfb.Property(hfb => hfb.Id)
                .HasColumnName("WeatherForecastHourlyId")
                .ValueGeneratedNever()
                .HasConversion( id => id.Value, value => WeatherForecastHourlyId.Create(value));
            });
        }

        private void ConfigureWeatherForecastTable(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.ToTable("WeatherForecasts");

            builder.HasKey(wf => wf.Id);
            builder.Property(wf => wf.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => WeatherForecastId.Create(value));
            builder.Property(wf => wf.Date)
                .HasColumnType("date");

        }
    }
}
