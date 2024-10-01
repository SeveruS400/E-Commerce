using Microsoft.EntityFrameworkCore;


namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        void Save();
        DbContext CreateDbContext();
    }
}
