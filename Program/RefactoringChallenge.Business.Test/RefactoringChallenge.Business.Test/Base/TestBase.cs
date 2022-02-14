using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RefactoringChallenge.Business.AutoMapper;
using RefactoringChallenge.Data.Contexts;

namespace RefactoringChallenge.Test.Base
{
    public abstract class TestBase
    {
        protected IMapper mapper;

        protected static NorthwindDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
               .UseInMemoryDatabase(databaseName: "Test")
               .Options;

            var db = new NorthwindDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        public TestBase()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            mapper = mapperConfig.CreateMapper();
        }
    }
}

