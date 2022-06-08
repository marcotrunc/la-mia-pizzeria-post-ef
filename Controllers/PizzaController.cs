using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Data;
using System.Net;
using System.Data;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public static PizzaList PizzaMenu = null;
        public static PizzaContext db = new PizzaContext();
        public ActionResult Index()
        {
            if(!db.Pizzas.Any())
            {
                Pizza FirstPizza = new Pizza()
                {
                    Title= "Margherita",
                    Description = "Pizza margherita",
                    Price= 7.99,
                    PhotoPath = "~/img/prima_pizza.jpg"
                };
                Pizza SecondPizza = new Pizza()
                {
                    Title = "Boscaiola",
                    Description = "Pizza Boscaiola",
                    Price = 8.99,
                    PhotoPath = "~/img/seconda_pizza.jpg"
                };
                Pizza ThirdPizza = new Pizza()
                {
                    Title = "Salsiccia",
                    Description = "Pizza Salsiccia",
                    Price = 9.99,
                    PhotoPath = "~/img/terza_pizza.jpg"
                }; 
                db.Add(FirstPizza);
                db.Add(SecondPizza);
                db.Add(ThirdPizza);
                db.SaveChanges();
            }
            
            return View(db);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            Pizza p = db.Pizzas.Find(id);
            ViewData["Pizza"] = p;
            return View(p);
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {

            Pizza NewPizza = new Pizza
            {
                Title = " ",
                Description = " ",
                Price = 0.00,
                PhotoPath =" "
            };
            return View(NewPizza);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Store(Pizza Result)
        {
            if(!ModelState.IsValid)
            {
                return View("Create",Result);
            }

            //Estrazione File e salvataggio su file system.
            //Agendo su Request ci prendiamo il file e lo salviamo su file system.
            string PhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

            if (!Directory.Exists(PhotoPath))
                Directory.CreateDirectory(PhotoPath);

            FileInfo fileInfo = new FileInfo(Result.File.FileName);
            string fileName = Result.Title + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(PhotoPath, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                Result.File.CopyTo(stream);
            }


            Pizza NewPizza = new Pizza()
            {
                Id = Result.Id,
                Title = Result.Title,
                Description = Result.Description,
                Price = Result.Price,
                PhotoPath =$"/Files/{fileName}",
            };

            db.Add(NewPizza);
            db.SaveChanges();
            ViewData["Pizza"] = NewPizza;
            return View("Details");
        }

        // GET: PizzaController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Pizza p = db.Pizzas.Find(id);
            return View(p);
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPizza(Pizza p)
        {
            if (!ModelState.IsValid)
                return View("Edit", p);
            string fileName = "";
            if (p.File.Length > 0)
            {

                string PhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files");

                if (!Directory.Exists(PhotoPath))
                    Directory.CreateDirectory(PhotoPath);
                FileInfo fileInfo = new FileInfo(p.File.FileName);
                fileName = p.Title + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(PhotoPath, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    p.File.CopyTo(stream);
                }
            }

            Pizza EditPizza = db.Pizzas.Find(p.Id);
            EditPizza.Title = p.Title;
            EditPizza.Description = p.Description;
            EditPizza.Price = p.Price;
            EditPizza.PhotoPath = $"/Files/{fileName}";
            db.SaveChanges();
            ViewData["Pizza"] = EditPizza;
            return View("Details");
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Pizza p = db.Pizzas.Find(id);
                db.Pizzas.Remove(p);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
