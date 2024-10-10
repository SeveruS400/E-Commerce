using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace araba_al_sat.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _serviceManager.ProductService.GetAllProducts(false);
            return View(model);
        }
        public async Task<IActionResult> Get(int Id)
        {
            var model = await _serviceManager.ProductService.GetOneProduct(Id, false);
            return View(model);
        }
    }
}
