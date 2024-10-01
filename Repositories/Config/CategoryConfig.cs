using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class CategoryConfig :IEntityTypeConfiguration<Categories>
    {

        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CategoryName).IsRequired();

            builder.HasData(
                new Categories() { Id = 1, CategoryName = " Book" },
                new Categories() { Id = 2, CategoryName = " Electronic" },
                new Categories() { Id = 3, CategoryName = " Music" }
            );
        }
    }
}
