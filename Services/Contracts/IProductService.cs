﻿using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAllProducts(bool trackChanges);
        IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p);
        IEnumerable<Products> GetShowCaseProducts(bool trackChanges);
        Task<Products>? GetOneProduct(int id, bool trackChanges);
        void CreateProduct(ProductDtoForInsertion productDto);
        Task UpdateProduct(ProductDtoForUpdate productDto);
        Task DeleteProduct(int id);
        Task<ProductDtoForUpdate> GetOneProductForUpdate(int id, bool trackChanges);
    }
}
