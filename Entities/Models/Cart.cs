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
        public void AddItem(Products products,int quantity)
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

        public void RemoveLine(Products products) => Lines.RemoveAll(l => l.Products.Id.Equals(products.Id));

        public decimal ComputeTotalValue() => Lines.Sum(e => e.Products.Price * e.Quantity);

        public void Clear() => Lines.Clear();
    }
}
