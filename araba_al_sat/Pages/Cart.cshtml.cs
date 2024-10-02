using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace e_commerce.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; }

        public CartModel(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            Cart = cart;
        }    
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public async Task<IActionResult> OnPost(int Id, string returnUrl)
        {
            Products? products = await _manager.ProductService.GetOneProduct(Id, false);
            if (products is not null)
            {
                Cart.AddItem(products, 1);
            }
            return Page();
        }
        public IActionResult OnPostRemove(int Id, string returnUrl)
        {
            var line = Cart.Lines.FirstOrDefault(cl => cl.Products.Id.Equals(Id));

            if (line != null)
            {
                Cart.RemoveLine(line.Products);
            }
            return Page();
        }
    }
}
