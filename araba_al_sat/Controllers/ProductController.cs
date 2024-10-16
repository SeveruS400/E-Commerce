using Entities.RequestParameters;
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

        public IActionResult Index(ProductRequestParameters p)
        {
            var model =  _serviceManager.ProductService.GetAllProductsWithDetails(p);
            return View(model);
        }
        public async Task<IActionResult> Get(int Id)
        {
            var model = await _serviceManager.ProductService.GetOneProduct(Id, false);
            return View(model);
        }
    }
}
