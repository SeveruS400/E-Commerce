using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        #region Ctor
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categorieRepository;
        private readonly IOrderRepository _orderRepository;

        public RepositoryManager(DataContext context, 
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICategoryRepository categorieRepository,
            IOrderRepository orderRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _categorieRepository = categorieRepository;
            _orderRepository = orderRepository;
        }
        #endregion

        public IUserRepository User => _userRepository;

        public ICategoryRepository Category => _categorieRepository;

        public IOrderRepository Order => throw new NotImplementedException();

        IProductRepository IRepositoryManager.Product => _productRepository;

        public DbContext CreateDbContext()
        {
            return _context;
        }

        public async void Save()
        {
            _context.SaveChangesAsync();
        }

        DbContext IRepositoryManager.CreateDbContext()
        {
            throw new NotImplementedException();
        }


    }
}
