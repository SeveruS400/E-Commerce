using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace araba_al_sat.Components
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public CategoriesMenuViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Category = await _serviceManager.CategoryService.GetAllCategories(false);
            return View(Category);
        }
    }
}
