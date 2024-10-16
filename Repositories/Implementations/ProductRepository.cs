using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using System.Collections.Generic;

namespace Repositories.Implementations
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async void CreateProduct(Products product) => await CreateAsync(product);

        public async Task<IEnumerable<Products>> GetAllProducts(bool trackChanges)
        {
            // trackChanges true ise veriler izlenir, değilse AsNoTracking ile performans kazanılır
            var products = trackChanges
                ? await _context.Products.ToListAsync()
                : await _context.Products.AsNoTracking().ToListAsync();

            return products;
        }

        public IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _context
                .Products
                .FilteredByCategoryId(p.CategoryId)
                .FilteredBySearchTerm(p.SearchTerm)
                .FilteredByPrice(p.MinPrice,p.MaxPrice, p.IsValidPrice);
        }

        public async Task<Products> GetOneProduct(int id, bool trackChanges)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Id == id);
            return product;
        }

        public IEnumerable<Products> GetShowCaseProducts(bool trackChanges)
        {
            return GetAll(trackChanges)
                .Where(p => p.ShowCase.Equals(true));
        }

        //public async void UpdateProduct(Products entity) => await UpdateAsync(entity.Id, entity);
    }
}
