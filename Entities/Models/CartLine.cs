using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CartLine :Base
    {
        public Products Products { get; set; } = new();
        public int Quantity { get; set; } 
    }
}
