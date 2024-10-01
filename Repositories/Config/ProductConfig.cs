using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();

            builder.HasData(
                new Products() { Id = 1, ProductName = "Computer", Price = 2000, CategoryId = 2, ImageUrl="/images/1.jpg" },
                new Products() { Id = 2, ProductName = "Mouse", Price = 1000, CategoryId = 2, ImageUrl = "/images/2.jpg" },
                new Products() { Id = 3, ProductName = "Keyboard", Price = 1000, CategoryId = 2, ImageUrl = "/images/3.jpg" },
                new Products() { Id = 4, ProductName = "Guitar", Price = 150, CategoryId = 3, ImageUrl = "/images/4.jpg" }
                );
        }
    }
}
