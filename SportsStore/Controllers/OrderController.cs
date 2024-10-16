using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository repository;

    private readonly Cart cart;

    public OrderController(IOrderRepository repository, Cart cart)
    {
        this.repository = repository;
        this.cart = cart;
    }

    public IActionResult Checkout()
    {
        return this.View(new Order());
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (this.cart.Lines.Count == 0)
        {
            this.ModelState.AddModelError(string.Empty, "Sorry, your cart is empty!");
        }

        if (this.ModelState.IsValid && order is not null)
        {
            order.Lines = this.cart.Lines.ToArray();
            this.repository.SaveOrder(order);
            this.cart.Clear();
            return this.RedirectToPage("/Completed", new { orderId = order.OrderID });
        }
        else
        {
            return this.View();
        }
    }
}
