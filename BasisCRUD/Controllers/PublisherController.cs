using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasisCRUD.Controllers
{

    public class PublisherController : Controller
    {
        private readonly DemoDbContext _demoDbContext;
        public PublisherController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }

        public IActionResult Index()
        {
            var allP = from publisher in _demoDbContext.Publishers select publisher;
            if (allP == null)
            {
                return NotFound();
            }
            return View(allP);
        }

        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _demoDbContext.Publishers.Add(obj);
                _demoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int? id){
            if (id == 0 || id == null){
                return NotFound();
            }
            var obj = _demoDbContext.Publishers.Find(id);
            if (obj == null) {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult UpDB(Publisher obj){

            if (ModelState.IsValid){
                _demoDbContext.Publishers.Update(obj);
                _demoDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id){
            if (id == 0 || id == null){
                return NotFound();
            }
            var obj = _demoDbContext.Publishers.Find(id);
            if (obj == null) {
                return NotFound();
            }
            _demoDbContext.Publishers.Remove(obj);
            _demoDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
