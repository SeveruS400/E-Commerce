using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        Task<IEnumerable<Products>> GetAllProducts(bool trackChanges);
        IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p);
        IEnumerable<Products> GetShowCaseProducts(bool trackChanges);
        Task<Products> GetOneProduct(int id, bool trackChanges);
        void CreateProduct(Products product);
        //void UpdateProduct(Products entity);
    }
}
