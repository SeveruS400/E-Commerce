using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CategoryRepository : RepositoryBase<Categories>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Categories>> GetAllCategories(bool trackChanges)
        {
            // trackChanges true ise veriler izlenir, değilse AsNoTracking ile performans kazanılır
            var Categories = trackChanges
                ? await _context.Categories.ToListAsync()
                : await _context.Categories.AsNoTracking().ToListAsync();

            return Categories;
        }
    
    }
}
