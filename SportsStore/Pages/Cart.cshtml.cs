using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
#pragma warning disable SA1649 // File name should match first type name
    public class CartModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly IStoreRepository repository;

        public CartModel(IStoreRepository repository, Cart cartService)
        {
            this.repository = repository;
            this.Cart = cartService;
        }

        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            this.ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = this.repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                this.Cart.AddItem(product, 1);
            }

            return this.RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            this.Cart.RemoveLine(this.Cart.Lines.First(cl => cl.Product.ProductID == productId).Product);
            return this.RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
