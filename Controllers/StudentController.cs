using DemoMVC2.Data;
using DemoMVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC2.Controllers
{
    public class StudentController : Controller
    {
        //khai bao ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
        public StudentController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Xay dung action tra ve danh sach Student
        public async Task<IActionResult> Index()
        {
            //Lay danh sach Student va tra ve View
            var model = await _context.Students.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student std)
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
            var Student = await _context.Students.FindAsync(id);
            if (Student == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            return View(Student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,StudentName")] Student std)
        {
            if (id != std.StudentID)
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
                    if (!StudentExists(std.StudentID))
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
            var std = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
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
            var std = await _context.Students.FindAsync(id);
            _context.Students.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}