using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RefactoringChallenge.Data.Contexts;

namespace RefactoringChallenge.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
            => services.AddDbContext<NorthwindDbContext>(options => options.UseSqlServer(connectionString));

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
            => AddApplicationDbContext(services, "name=ConnectionStrings:DefaultConnection");
    }
}
