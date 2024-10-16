﻿using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;

        public OrderController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var orders = _manager.OrderService.Orders;
            return View(orders);
        }
        [HttpPost]
        public IActionResult Complete([FromForm] int id)
        {
            _manager.OrderService.Complate(id);
            return RedirectToAction("Index");
        }
    }
}
