using DemoMVC2.Data;
using DemoMVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC2.Controllers
{
    public class PersonController : Controller
    {
        //khai bao ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonID == id);
        }
        public PersonController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Xay dung action tra ve danh sach Student
        public async Task<IActionResult> Index()
        {
            //Lay danh sach Student va tra ve View
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person std)
        {
            if(ModelState.IsValid)
            {
                //Add vao ApplicationDbContext
                _context.Add(std);
                //luu thay doi vao db
                await _context.SaveChangesAsync();
                //sau khi luu thay doi, dieu huong ve trang index
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(String id)
        {
            if (id == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            var Person = await _context.Persons.FindAsync(id);
            if (Person == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            return View(Person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,PersonName")] Person std)
        {
            if (id != std.PersonID)
            {
                //return NotFound();
                return View("NotFound");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(std);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(std.PersonID))
                    {
                        //return NotFound();
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(std);
        }
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            var std = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (std == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            return View(std);
        }
        // PORT: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var std = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}