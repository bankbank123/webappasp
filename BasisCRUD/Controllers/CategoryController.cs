using Microsoft.AspNetCore.Mvc;

namespace BasisCRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DemoDbContext _demoDbContext;
        public CategoryController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        public IActionResult Index()
        {
            var allC = from cat in _demoDbContext.Categories select cat;
            if (allC == null)
            {
                return NotFound();
            }
            return View(allC);
        }

        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _demoDbContext.Categories.Add(obj);
                _demoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var obj = _demoDbContext.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult UpDB(Category obj) {
            if (ModelState.IsValid)
            {
                _demoDbContext.Categories.Update(obj);
                _demoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id) {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var obj = _demoDbContext.Categories.Find(id);
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
