using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Products> FilteredByCategoryId(this IQueryable<Products> products, int? categoryId)
        {
            if (categoryId is null)
                return products;
            else
                return products.Where(p => p.CategoryId.Equals(categoryId));
        }
        public static IQueryable<Products> FilteredBySearchTerm(this IQueryable<Products> products,
     String? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;
            else
                return products.Where(prd => prd.ProductName.ToLower()
                    .Contains(searchTerm.ToLower()));
        }

        public static IQueryable<Products> FilteredByPrice(this IQueryable<Products> products,
            int minPrice, int maxPrice, bool isValidPrice)
        {
            if (isValidPrice)
                return products.Where(prd => prd.Price >= minPrice && prd.Price <= maxPrice);
            else
                return products;
        }

        public static IQueryable<Products> ToPaginate(this IQueryable<Products> products,
            int pageNumber, int pageSize)
        {
            return products
                .Skip(((pageNumber - 1) * pageSize))
                .Take(pageSize);
        }
    }
}
