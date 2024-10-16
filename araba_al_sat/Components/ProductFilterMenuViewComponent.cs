using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Components
{
    public class ProductFilterMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
