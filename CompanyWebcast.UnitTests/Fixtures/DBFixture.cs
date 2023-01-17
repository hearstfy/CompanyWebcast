using CompanyWebcast.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace CompanyWebcast.UnitTests.Fixtures
{
    public class DatabaseFixture: IDisposable
    {
        private readonly ApplicationDBContext _context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDBContext(options);
            _context.Database.EnsureCreated();
        }
        public  ApplicationDBContext GetDbContext()
        {
            return _context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
