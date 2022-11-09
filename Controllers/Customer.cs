using DemoMVC2.Data;
using DemoMVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC2.Models.Process;

namespace DemoMVC2.Controllers
{
    public class CustomerController : Controller
    {
        //khai bao ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Xay dung action tra ve danh sach Student
        public async Task<IActionResult> Index()
        {
            //Lay danh sach Student va tra ve View
            var model = await _context.Customers.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer std)
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
            var Customer = await _context.Customers.FindAsync(id);
            if (Customer == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,CustomerName")] Customer std)
        {
            if (id != std.CustomerID)
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
                    if (!CustomerExists(std.CustomerID))
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                //return NotFound();
                return View("NotFound");
            }
            var std = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
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
            var std = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(std);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var FileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", FileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var std = new Customer();

                            std.CustomerID = dt.Rows[i][0].ToString();
                            std.CustomerName = dt.Rows[i][1].ToString();
                            std.Age = dt.Rows[i][2].ToString();

                            _context.Customers.Add(std);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}
    