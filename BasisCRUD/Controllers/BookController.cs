using BasisCRUD.Models;
using Microsoft.AspNetCore.Http;
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
        // GET: BooksController
        public async Task<ActionResult> Index()
        {
            var allB = _demoDbContext.Books
                .Include(book => book.Category)
                .Include(book => book.Publish);
            return View(await allB.ToListAsync());
        }

        // GET: BooksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            var book = await _demoDbContext.Books
            .Include(book => book.Category)
            .Include(book => book.Publish)
            .FirstOrDefaultAsync(book => book.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            if (ModelState.IsValid)
            {
                try
                {
                    _demoDbContext.Books.Add(book);
                    await _demoDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return View();
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var allP = _demoDbContext.Publishers.ToList();
            var allC = _demoDbContext.Categories.ToList();
            ViewBag.Categories = allC;
            ViewBag.Publishers = allP;
            var book = _demoDbContext.Books
            .Include(book => book.Category)
            .Include(book => book.Publish)
            .FirstOrDefaultAsync(book => book.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            return View(await book);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _demoDbContext.Books.Update(book);
                    await _demoDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExisits(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExisits(int id)
        {
            return _demoDbContext.Books.Any(book => book.BookId == id);
        }

        // GET: BooksController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _demoDbContext.Books
                .Include(book => book.Publish)
                .Include(book => book.Category)
                .FirstOrDefaultAsync(book => book.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComfiremd(int id)
        {
            var book = await _demoDbContext.Books.FindAsync(id);
            Console.WriteLine(id);
            if (book == null)
            {
                return NotFound();
            }
            _demoDbContext.Books.Remove(book);
            await _demoDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
