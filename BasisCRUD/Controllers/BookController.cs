using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BasisCRUD.Controllers
{

    public class BookController : Controller
    {
        private readonly DemoDbContext _demoDbContext;
        public BookController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }

        public IActionResult Index()
        {
            var allB = _demoDbContext.Books
            .Include(book => book.Category) // Ensure Category is loaded
            .Include(book => book.Publish) // Ensure Publisher is loaded
            .ToList();
            return View(allB);
        }

        public IActionResult Create(Book obj)
        {
            // Retrieve all publishers and categories for the dropdown lists
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
        
            if (ModelState.IsValid)
            {
                _demoDbContext.Books.Add(obj);
                _demoDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _demoDbContext.Books.Find(id);
            if (obj == null) {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Book obj)
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            if (ModelState.IsValid)
            {
                _demoDbContext.Books.Update(obj);
                _demoDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _demoDbContext.Books.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _demoDbContext.Remove(obj);
            _demoDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}