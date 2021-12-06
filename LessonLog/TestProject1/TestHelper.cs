using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonLog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class TestHelper
    {
        private readonly LessonLogContext _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<LessonLogContext>();
            builder.UseInMemoryDatabase(databaseName: "LessonLogDB");

            var dbContextOptions = builder.Options;
            _context = new LessonLogContext(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public LessonLogContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}