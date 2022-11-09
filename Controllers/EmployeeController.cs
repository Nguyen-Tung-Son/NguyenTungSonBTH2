using DemoMVC2.Data;
using DemoMVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC2.Models.Process;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get: employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xln" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to server
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var steam = new FileStream(filePath,FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Employee object
                            var emp = new Employee();
                            // set values for attributes
                            emp.EmployeeID = dt.Rows[i][0].ToString();
                            emp.EmployeeName = dt.Rows[i][1].ToString();
                            emp.Address = dt.Rows[i][2].ToString();
                            // add object to Context
                            _context.Employees.Add(emp);
                        }
                        // save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}
