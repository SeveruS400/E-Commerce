using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cart : Base
    {
        
        public List<CartLine> Lines { get; set; }
        public Cart() 
        {
            Lines = new List<CartLine>();
        }
        public virtual void AddItem(Products products,int quantity)
        {
            CartLine? line = Lines.Where(l => l.Products.Id.Equals(products.Id)).FirstOrDefault();
             if(line is null)
            {
                Lines.Add(new CartLine()
                {
                    Products = products,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Products products) => Lines.RemoveAll(l => l.Products.Id.Equals(products.Id));

        public virtual void DecreaseQuantity(Products products)
        {
            var line = Lines.FirstOrDefault(l => l.Products.Id == products.Id);

            if (line != null)
            {
                if (line.Quantity > 1)
                {
                    line.Quantity--;
                }
                else
                {
                    Lines.Remove(line);
                }
            }
        }

        public decimal ComputeTotalValue() => Lines.Sum(e => e.Products.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();
    }
}
