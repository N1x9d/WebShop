using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;

namespace WebShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var bvm = new BaseViewModel();
        return View(bvm);
    }

    public IActionResult Product(Guid ProductId)
    {
       var p = DAO.GetProduct(ProductId).Result;
        return PartialView(p);
    }

    public IActionResult Products(string Type)
    {
        var storeModel = new StoreModel();
        storeModel.Categories = DAO.GetTypesInfo().Result;
        var g = storeModel.Categories.First(c => c.Type.ToString() == Type);
        g.IsSelected = true;
        ProductType type = (ProductType)Enum.Parse(typeof(ProductType), Type);
        storeModel.Products = DAO.GetProducts(type).Result;
        storeModel.Brands = new List<BrandModel>();
        var brandsName= storeModel.Products.Select(c => c.Brand).Distinct().ToList();

        foreach(var brandName in brandsName)
        {
            var count = storeModel.Products.Select(c => c.Brand == brandName).Count();
            storeModel.Brands.Add(new BrandModel { Count = count, Name = brandName });
        }

        return PartialView(storeModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
