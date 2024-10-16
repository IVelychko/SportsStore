using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository repository;

    public HomeController(IStoreRepository repository)
    {
        this.repository = repository;
    }

    public int PageSize { get; set; } = 4;

    public IActionResult Index(string? category, int productPage = 1)
    {
        return this.View(new ProductsListViewModel
        {
            Products = this.repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * this.PageSize)
                .Take(this.PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = this.PageSize,
                TotalItems = this.repository.Products.Count(p => category == null || p.Category == category),
            },
            CurrentCategory = category,
        });
    }
}
