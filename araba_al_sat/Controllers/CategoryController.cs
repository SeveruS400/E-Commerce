using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace e_commerce.Controllers
{
    public class CategoryController : Controller
    {
        private IRepositoryManager _manager;

        public CategoryController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var model = _manager.Category.GetAll(false);
            return View(model);
        }
    }
}
