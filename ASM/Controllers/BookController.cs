using ASM.Data;
using ASM.Models;
using ASM.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class BookController : Controller
    {
        private readonly ASMContext dbcontext;

        public BookController(ASMContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var books = await dbcontext.Book
                .Include(b => b.Category)
                .ToListAsync();

            return View(books);
        }
        public async Task<IActionResult> UserIndexAsync()
        {
            var books = await dbcontext.Book
                .Include(b => b.Category)
                .ToListAsync();

            return View(books);
        }

        public async Task<IActionResult> UserSearch(string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;
            var books = from s in dbcontext.Book
                        .Include(s => s.Category)
                        select s;
            books = books.Where(s => s.Title.Contains(searchString));
            List<Book> booksList = await books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new CreateBookViewModel();
            var categories = await dbcontext.Category.ToListAsync();

            model.Categories = new SelectList(categories, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateBookViewModel model)
        {
            // check model is valid
            if (ModelState.IsValid)
            {
                // if true then save to database
                Book book = model.Book;

                dbcontext.Book.Add(book);

                await dbcontext.SaveChangesAsync();
            }
            else
            {
                // load categories again to model and return to view with errors.
                var categories = await dbcontext.Category.ToListAsync();

                model.Categories = new SelectList(categories, "Id", "Name");

                return View(model);
            }

            // Redirect to database

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await dbcontext.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await dbcontext.Book.FindAsync(id);
            dbcontext.Book.Remove(book);
            await dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
