using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace araba_al_sat.Components
{
    public class ProductSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public ProductSummaryViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<string> InvokeAsync()
        {
            var products = await _serviceManager.ProductService.GetAllProducts(false);
            return products.Count().ToString();
        }
    }
}
