using Microsoft.AspNetCore.Mvc;
using Products_za_ocenka.Data;
using Products_za_ocenka.Data.Models;
using Products_za_ocenka.Models;

namespace Products_za_ocenka.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Quantity= model.Quantity
            };
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }

        public IActionResult All()
        {
            List<ProductViewModel> model = db.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Product product = db.Products.FirstOrDefault(x => x.Id == id);
            db.Products.Remove(product);
            db.SaveChanges();
            return Redirect("/Product/All");
        }
    }
}
