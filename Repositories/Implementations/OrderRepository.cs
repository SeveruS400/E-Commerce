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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(cl => cl.Products)
            .OrderBy(o => o.Shipped)
            .ThenByDescending(o => o.Id);

        public int NumberOfInProcess => _context.Orders.Count(o => o.Shipped.Equals(false));

        public void Complate(int id)
        {
            var order = FindByCondition(o => o.Id.Equals(id),true);
            if (order is null)
                throw new Exception("Order not found");
            order.Shipped = true;
            _context.SaveChanges();
        }

        public Order? GetOneOrder(int id)
        {
            return FindByCondition(o => o.Id.Equals(id),false);
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(o => o.Products));
            if(order.Id == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();

        }
    }
}
