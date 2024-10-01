using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using System.Diagnostics;

namespace araba_al_sat.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IServiceManager serviceManager, IWebHostEnvironment webHostEnvironment)
        {
            _serviceManager = serviceManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _serviceManager.ProductService.GetAllProducts(false);
            return View(model);
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _serviceManager.CategoryService.GetAllCategories(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //file op
                string path = Path.Combine(_webHostEnvironment.WebRootPath,"images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _serviceManager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update([FromRoute(Name = "ID")] int id)
        {
            ViewBag.Category = await _serviceManager.CategoryService.GetAllCategories(false);
            var model = await _serviceManager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //file op
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                await _serviceManager.ProductService.UpdateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute(Name = "ID")] int id)
        {
            var product = await _serviceManager.ProductService.GetOneProduct(id, trackChanges: false);
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                // wwwroot/images/ ile başlayan yol ise, fiziksel tam dosya yolunu al
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));

                // Dosyanın var olup olmadığını kontrol et ve varsa sil
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            await _serviceManager.ProductService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
